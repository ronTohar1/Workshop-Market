using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts;
using System;
using System.Collections.Concurrent;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class Store
    {
        public string name { get; }
        public Member founder { get; }
        public bool isOpen { get; private set; }
        public Hierarchy<int> appointmentsHierarchy { get; }
        public virtual IDictionary<int, Product> products { get; }

        private IList<Purchase> purchaseHistory;
        private IDictionary<int, IList<Permission>> managersPermissions;
        private IDictionary<Role, IList<int>> rolesInStore;
        private Func<int, Member> membersGetter;
        private Mutex isOpenMutex;


        private const int timeoutMilis = 2000; // time for wating for the rw lock in the next line, after which it throws an exception
        private ReaderWriterLock rolesAndPermissionsLock;

        private IDictionary<int, IDictionary<Product,int>> transactions;
        private ConcurrentDictionary<int,Mutex> productsMutex;
        private Mutex transactionIdMutex;

        public StoreDiscountPolicyManager discountManager { get; }
        public virtual StorePurchasePolicyManager purchaseManager { get; }


        // cc 5
        // cc 6
        public Store(string storeName, Member founder, Func<int, Member> membersGetter)
	    {
            discountManager = new StoreDiscountPolicyManager();
            purchaseManager = new StorePurchasePolicyManager();

            this.name = storeName;
            this.founder = founder;
            this.isOpen = true;
            this.appointmentsHierarchy = new Hierarchy<int>(founder.Id);
            this.purchaseHistory = new SynchronizedCollection<Purchase>();
            this.products = new ConcurrentDictionary<int, Product>();
            this.managersPermissions = new ConcurrentDictionary<int, IList<Permission>>();
            initializeRolesInStore();
            this.membersGetter = membersGetter;
            this.isOpenMutex = new Mutex();

            //Transactions
            this.transactions = new Dictionary<int, IDictionary<Product, int>>();
            this.productsMutex = new();
            this.transactionIdMutex = new Mutex();


            this.rolesAndPermissionsLock = new ReaderWriterLock(); // no need to acquire it here (probably) because constructor is of one thread
        }

        public bool CommitTransaction(int transactionId)
        {
            if (transactions.ContainsKey(transactionId))
                transactions.Remove(transactionId);
            else
                return false;
            return true;
        }
        //getter
        public List<Product> GetTransactionProducts(int transactionId)
        {
            List<Product> productsPrices = new();
            if (!transactions.ContainsKey(transactionId))
                return null;

            foreach (Product prod in transactions[transactionId].Keys)
                productsPrices.Add(prod);
            return productsPrices;
        }

        public bool RollbackTransaction(int transactionId)
        {
            if (transactions.ContainsKey(transactionId))
            {
                foreach (var prod in transactions[transactionId])
                {
                    Product product = prod.Key;
                    int amount = prod.Value;
                    product.AddToInventory(amount);
                }
                transactions.Remove(transactionId);
            }
            else
                return false;
            return true;
            
        }
        
        // Reserving products.
        // If successful then assigning the transactionId, else returns informative string why failed.
        public string? ReserveProducts(int buyerId,IDictionary<int,int> productsAmounts,out int transactionId)
        {
            //Sorting by product id so there will be no mismatches when trying to decrease amount in different purchases
            SortedDictionary<int, int> sortedProducts = new(productsAmounts);
            string? canBuy = null;
            int transaction = CreateNewTransaction();

            foreach (KeyValuePair<int, int> product in sortedProducts)
            {
                if (transaction != -1)
                {
                    int prodId = product.Key;
                    int amount = product.Value;
                
                    string? canBuyProduct = ReserveSingleProduct(buyerId, prodId, amount);
                    if (canBuyProduct != null)
                    {
                        canBuy += canBuyProduct;

                        if (transaction != -1)
                        {
                            RevertTransaction(transaction);
                            transactions.Remove(transaction);
                            transaction = -1;
                        }
                    }
                    else
                    {
                        Product productObj = this.SearchProductByProductId(prodId);
                        if (productObj == null)
                            throw new Exception($"A shopping bag contained a product that is not in the store!\nProductId: {prodId}, Store name: {this.name}");
                        transactions[transaction].Add(productObj, amount);
                    }
                }
            }
            transactionId = transaction;
            return canBuy;

        }

        private void RevertTransaction(int transaction) {
            foreach (Product p in transactions[transaction].Keys) 
                p.AddToInventory(transactions[transaction][p]);
        }
        // Removing the amount from the prodcut in the inventory 
        private string? ReserveSingleProduct(int buyerId, int productId, int amount)
        {
            if (!products.ContainsKey(productId))
                return $"Product with id {productId} doesn't exist in store {this.name}";
            lock (products[productId].storeMutex)
            {
                string? canBuy = CanBuyProduct(buyerId, productId, amount);

                if (canBuy != null)
                    return canBuy;

                try
                {
                    DecreaseProductAmountFromInventory(this.founder.Id, productId, amount);
                }
                catch (Exception e)
                {
                    return "Could not buy product: " + products[productId].name + ". An unexpected error has occurd : "+e.Message;
                }
            }
            return null;
        }

        private int CreateNewTransaction()
        {
            lock (transactionIdMutex) {
                int maxId = transactions.Count == 0 ? 0: transactions.Keys.Max();
                transactions.Add(maxId + 1, new Dictionary<Product, int>());
                return maxId + 1;
            }
        }

        private void initializeRolesInStore()
        {
            // need to be called from constructor or with acquiring the lock 
            // saving founder as a coOnwer as well
            if (founder == null)
                throw new ArgumentNullException("Initializing roles in stores should happen after founder is initialized");
            this.rolesInStore = new ConcurrentDictionary<Role, IList<int>>();
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                rolesInStore.Add(role, new SynchronizedCollection<int>());
            }
            this.rolesInStore[Role.Owner].Add(founder.Id);
        }

        public virtual string GetName()
        {
            return name;
        }

        // ------------------------------ Products and Policy ------------------------------
        private void EnforceAtLeastCoOwnerPermission(int memberId, string message) {
            string permissionError = CheckAtLeastCoOwnerPermission(memberId);
            if (permissionError != null)
                throw new MarketException(StoreErrorMessage(message + permissionError));
        }

        // r.4.1
        public int AddNewProduct(int memberId, string productName, double pricePerUnit, string category) {
            // we allow this only to coOwners
            EnforceAtLeastCoOwnerPermission(memberId, "Could not add a new product: ");
            if (pricePerUnit <= 0)
                throw new Exception("Cannot add product with price smaller or equal to 0!");
            Product newProduct = new Product(productName, pricePerUnit, category);
            products.Add(newProduct.id, newProduct);
            return newProduct.id;
        }
        // r.4.1
        public virtual void AddProductToInventory(int memberId, int productId, int amount) {
            // we allow this only to coOwners
            EnforceAtLeastCoOwnerPermission(memberId, "Could not add to inventory: ");
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not add to inventory: there isn't such a product with product id: {productId}"));
            products[productId].AddToInventory(amount);
        }
        // r.4.1
        public void RemoveProduct(int memberId, int productId) {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not remove a product: ");
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not remove a product: there isn't such a product with product id: {productId}"));
            products.Remove(productId);
        }
        // r.4.1
        // c.9
        public virtual void DecreaseProductAmountFromInventory(int memberId, int productId, int amount)
        {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not take from inventory: ");
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not take from inventory: there isn't such a product with product id: {productId}"));
            try {
                products[productId].RemoveFromInventory(amount);
            }
            catch (MarketException mEx) {
                throw new MarketException(StoreErrorMessage($"Could not take from inventory: {mEx.Message}"));
            }
        }

        public IList<Product> SerachProducts(ProductsSearchFilter filter)
        => products.Values.Where(p => filter.FilterProduct(p)).ToList();


        // r.4.1
        public void SetProductPrice(int memberId, int productId, double productPrice)
        {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not set the product price per unit: ");
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not set the product price per unit: there isn't such a product with product id: {productId}"));
            products[productId].SetProductPriceByUnit(productPrice);
        }
        // r.4.1
        public void SetProductDiscountPercentage(int memberId, int productId, double discountPercentage)
        {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not set the product's discount percentage: ");
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not set the product's discount percentage: there isn't such a product with product id: {productId}"));
            products[productId].SetProductDiscountPercentage(discountPercentage);
        }
        // r.4.1
        public void SetProductCategory(int memberId, int productId, string category)
        {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not set the product's category: ");
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not set the product's category: there isn't such a product with product id: {productId}"));
            products[productId].SetProductCategory(category);
        }
        // r.3.3
        // r.1.5, 1.6
        public void AddProductReview(int memberId, int productId, string review) {
            string permissionError = CheckAtLeastMemberPermission(memberId);
            if (permissionError != null)
                throw new MarketException("Could not add review: " + permissionError);
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not add review: there isn't such a product with product id: {productId}"));
            products[productId].AddProductReview(memberId, review);
            notifyAllStoreOwners($"The member with id: {memberId} has written a new review of a product woth id: {productId} at {this.name}");
        }
        // 6.4, 4.13
        public virtual void AddPurchaseRecord(int memberId, Purchase purchase)
        {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not add purchase option for the product: ");
            purchaseHistory.Add(purchase);
        }

        // TODO: amit and david should disscus it
        public IList<Purchase> GetPurchaseHistory()
        {
            lock (isOpenMutex)
            {
                if (!isOpen)
                    throw new MarketException($"Could not recieve purchase history: {this.name} is closed");
                return purchaseHistory;
            }
        }

        // 6.4, 4.13
        public IList<Purchase> GetPurchaseHistory(int memberId)
        {
            EnforceAtLeastCoOwnerPermission(memberId, "could not get the purchase history: ");
            lock (isOpenMutex)
            {
                if (!isOpen)
                    throw new MarketException($"Could not recieve purchase history: {this.name} is closed");
                return purchaseHistory;
            }
          
        }
        // r.3.3
        public IDictionary<Member, IList<string>> GetProductReviews(int productId)
        {
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not get reviews: there isn't such a product with product id: {productId}"));
            lock (isOpenMutex)
            {
                if (!isOpen)
                    throw new MarketException($"Could not recieve purchase history: {this.name} is closed");

                IDictionary<int, IList<string>> memberIdToReviews = products[productId].reviews;
                IDictionary< Member, IList<string>> membersToReviews = new Dictionary<Member, IList<string>>();
                foreach (int memberId in memberIdToReviews.Keys) {
                    Member? m = membersGetter(memberId);
                    if (m != null)
                        membersToReviews[m] = memberIdToReviews[memberId];
                }
                return membersToReviews;
            }
        }

        // r.3.3
        //recieves shopping bag and calculates the total to pay, consideroing all the restroctions
        public virtual Tuple<double,double> GetTotalBagCost(ShoppingBag shoppingBag) 
        {
            foreach (var product in shoppingBag.ProductsAmounts)
            {   
                int productId = product.Key.ProductId;
                int productAmount = product.Value;

                if (!products.ContainsKey(productId))
                    throw new MarketException(StoreErrorMessage($"Could not calculate bag total to pay: there isn't such a product"));
            }
            double productsTotalPrices = shoppingBag.ProductsAmounts.Keys.Sum(prodInBag => products[prodInBag.ProductId].GetPrice() * shoppingBag.ProductsAmounts[prodInBag]);
            double amountDiscount = discountManager.EvaluateDiscountForBag(shoppingBag,this);
            return new(productsTotalPrices ,amountDiscount);
        }

        public virtual string? CanBuyProduct(int buyerId, int productId, int amount)
        {
            if (!products.ContainsKey(productId))
                return $"The product can't be bought in the {name} store, there isn't such a product with id: {productId}";
            string productPurchaseFailMessage = "The product can't be bought: ";
            bool productCanBePurchased = true;
            if (products[productId].amountInInventory == 0)
            {
                productPurchaseFailMessage = productPurchaseFailMessage + $"\n     there arn't any {products[productId].name} currently at the inventory";
                productCanBePurchased = false;
            }
            else if (amount > products[productId].amountInInventory)
            {
                productPurchaseFailMessage = productPurchaseFailMessage + $"\n     there are only {products[productId].amountInInventory} products of {products[productId].name} at the inventory";
                productCanBePurchased = false;
            }
            if (productCanBePurchased)
                return null;

            return productPurchaseFailMessage;


        }

        //------------------------- search products within shop --------------------------

        // r 2.2
        public IList<Product> SearchProducts(ProductsSearchFilter filter)
        => products.Values.Where(p => filter.FilterProduct(p)).ToList();
        
        // ------------------------------ Permission and Roles ------------------------------

        // cc 3
        // r 4.4
        public void MakeCoOwner(int requestingMemberId, int newCoOwnerMemberId) {
            rolesAndPermissionsLock.AcquireWriterLock(timeoutMilis);
            string permissionError = CheckAtLeastCoOwnerPermission(requestingMemberId);
            if (permissionError != null)
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException("Could not make owner: " + permissionError);
            }

            if (IsCoOwner(newCoOwnerMemberId))
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException(StoreErrorMessage("The member is already a CoOwner"));
            }
            if (!IsMember(newCoOwnerMemberId))
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException("The requested new CoOwner is not a member");
            }

            // todo: check if okay to appoint manager to coOwner, according to  
            // the requirements, the hierarchy (also notice it stays where it was),
            // permissions etc. 
            if (IsManager(newCoOwnerMemberId)) // removing from being a manager
            {
                Hierarchy<int> managerInHierarchy = appointmentsHierarchy.FindHierarchy(newCoOwnerMemberId);
                int parentId = managerInHierarchy.parent.value; 
                if (parentId != requestingMemberId)
                {
                    rolesAndPermissionsLock.ReleaseWriterLock();
                    throw new MarketException("The requesting member is not the one that appointed the member to a manager, so can not appoint it to be a coOwner");
                    // todo: also add tests for when the appointing coOwner is not the one that appointed the member of newCoOwnerId to be a manager
                }

                managersPermissions.Remove(newCoOwnerMemberId);
                rolesInStore[Role.Manager].Remove(newCoOwnerMemberId); 
            }
            else
            {
                appointmentsHierarchy.AddToHierarchy(requestingMemberId, newCoOwnerMemberId);
                // todo: add tests checking this field has been changed (and for what happens when newCoOwnerId is of a manager)
            }

            rolesInStore[Role.Owner].Add(newCoOwnerMemberId);
            
            rolesAndPermissionsLock.ReleaseWriterLock();
            // todo: check that this mutex is synchronizing all these things okay
        }

        // r 4.5
        public void RemoveCoOwner(int requestingMemberId, int toRemoveCoOwnerMemberId)
        {
            rolesAndPermissionsLock.AcquireWriterLock(timeoutMilis);
            string permissionError = CheckAtLeastCoOwnerPermission(requestingMemberId);
            if (permissionError != null)
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException("Could not remove co owner: " + permissionError);
            }
            
            permissionError = CheckAtLeastCoOwnerPermission(toRemoveCoOwnerMemberId);
            if (permissionError != null)
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException("Could not remove co owner: " + permissionError);
            }

            Hierarchy<int> removedBrance = appointmentsHierarchy.RemoveFromHierarchy(requestingMemberId, toRemoveCoOwnerMemberId);
            RemovedByOwnerBranchUpdate(removedBrance, $"We regeret to inform you that you've lost your position at {this.name}");

            rolesAndPermissionsLock.ReleaseWriterLock();
            
        }
        private void RemovedByOwnerBranchUpdate(Hierarchy<int> removedBranch, string notification) {
            if (removedBranch == null)
                return;
            int memberId = removedBranch.value;
            rolesInStore[Role.Owner].Remove(memberId);//doesn't do anything if not in collection
            rolesInStore[Role.Manager].Remove(memberId);
            membersGetter(memberId).Notify(notification);
            foreach (Hierarchy<int> child in removedBranch.children) {
                RemovedByOwnerBranchUpdate(child, notification);
            }
        }

        // cc 3
        // r 4.6, r 5
        public void MakeManager(int requestingMemberId, int newCoManagerMemberId)
        {
            rolesAndPermissionsLock.AcquireWriterLock(timeoutMilis); 
            string permissionError = CheckAtLeastManagerWithPermission(requestingMemberId, Permission.MakeCoManager);
            if (permissionError != null)
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException("Could not make manager: " + permissionError);
            }
            if (IsCoOwner(newCoManagerMemberId))
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException(StoreErrorMessage("The member is already a CoOwner"));
            }
            if (IsManager(newCoManagerMemberId))
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException(StoreErrorMessage("The member is already a Manager"));
            }
            if (!IsMember(newCoManagerMemberId))
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException("The requested new CoOwner is not a member");
            }

            rolesInStore[Role.Manager].Add(newCoManagerMemberId);


            managersPermissions[newCoManagerMemberId] = DefualtManagerPermissions(); 

            appointmentsHierarchy.AddToHierarchy(requestingMemberId, newCoManagerMemberId);
            // todo: add tests checking this field has been changed

            rolesAndPermissionsLock.ReleaseWriterLock();
            // todo: check that this mutex is synchronizing all these things okay
        }

        private IList<Permission> DefualtManagerPermissions()
        {
            // no need to lock because not accessing fields (and also the calling function calls after aquireing the lock)
            IList<Permission> managerPermissions = new SynchronizedCollection<Permission>();
            managerPermissions.Add(Permission.RecieveInfo); // dfault managerPermissions
            return managerPermissions;
        }

        // r 4.7, r 5
        public void ChangeManagerPermissions(int requestingMemberId, int managerMemberId, IList<Permission> newPermissions) {
            rolesAndPermissionsLock.AcquireWriterLock(timeoutMilis);
            // we allow this only to coOwners
            string permissionError = CheckAtLeastCoOwnerPermission(requestingMemberId);
            if (permissionError != null)
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException("Could not changed manager permissions: " + permissionError);
            }

            if (!IsManager(managerMemberId))
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException(StoreErrorMessage("The id: " + managerMemberId + " is not of a managaer"));
            }
            managersPermissions[managerMemberId] = newPermissions;

            rolesAndPermissionsLock.ReleaseWriterLock();
            // todo: check that this mutex is synchronizing all these things okay
        }

        // r 4.11, r 5
        public IList<int> GetMembersInRole(int memberId, Role role) {
            string permissionError = CheckAtLeastManagerWithPermission(memberId, Permission.RecieiveRolesInfo); 
            if (permissionError != null)
                throw new MarketException("Error in getting members in role: " + role.ToString() + " " + permissionError);
            lock (isOpenMutex)
            {
                if (!isOpen)
                    throw new MarketException($"Could not check members in role: {this.name} is closed");

                // no need to aquire lock because the second action does not depend on the first
                IList<int> rollers = rolesInStore[role];
                return new List<int>(rollers);
            }
        }

        // r 4.11 r 5
        public Member GetFounder(int memberId)
        {
            string permissionError = CheckAtLeastManagerWithPermission(memberId, Permission.RecieiveRolesInfo);
            if (permissionError != null)
                throw new MarketException("Error in getting founder: " + permissionError);

            // no need to aquire lock because the second action does not depend on the first

            return founder;
        }

        // r 4.11, r 5
        public IList<Permission> GetManagerPermissions(int requestingMemberId, int managerMemberId)
        {
            string permissionError = CheckAtLeastManagerWithPermission(requestingMemberId, Permission.RecieiveRolesInfo);
            if (permissionError != null)
                throw new MarketException("Error in getting manager permissions: " + permissionError);

            rolesAndPermissionsLock.AcquireReaderLock(timeoutMilis);
            if (!IsManager(managerMemberId))
            {
                rolesAndPermissionsLock.ReleaseReaderLock();
                throw new MarketException("This is not a manager so its permissions could not be retunrd");
            }
            lock (isOpenMutex)
            {
                if (!isOpen)
                    throw new MarketException($"Could not recieve purchase history: {this.name} is closed");

                IList<Permission> result = new List<Permission>(managersPermissions[managerMemberId]);
                rolesAndPermissionsLock.ReleaseReaderLock();
                return result;
            }
        }

        public bool IsFounder(int memberId)
        {
            return founder.Id == memberId;
        }

        public bool IsCoOwner(int memberId)
        {
            return rolesInStore[Role.Owner].Contains(memberId);
        }

        public bool IsManager(int memberId)
        {
            return rolesInStore[Role.Manager].Contains(memberId);
        }

        // todo: maybe write tests about these methods

        private bool HasPermission(int managerId, Permission permission)
        {
            rolesAndPermissionsLock.AcquireReaderLock(timeoutMilis);
            if (!IsManager(managerId))
            {
                rolesAndPermissionsLock.ReleaseReaderLock();
                throw new ArgumentException(StoreErrorMessage("The id: " + managerId + " is not of a managaer"));

            }
            bool result = managersPermissions[managerId].Contains(permission);
            rolesAndPermissionsLock.ReleaseReaderLock();
            return result; 
        }

       
        private bool IsManagerWithPermission(int memberId, Permission permission)
        {
            rolesAndPermissionsLock.AcquireReaderLock(timeoutMilis);
            bool result = IsManager(memberId) && HasPermission(memberId, permission);
            return result;
        }
     
        // ----------------------------- Discounts policy -----------------------------

        public int AddDiscountPolicy(IExpression exp, string descrption, int memberId)
        {
            //TODO check if permission alows to handle discounts
            string permissionError = CheckAtLeastManagerWithPermission(memberId,Permission.DiscountPolicyManagement);
            if (permissionError != null)
                throw new MarketException("Could not add discount policy: " + permissionError);
            
            int id = discountManager.AddDiscount(descrption, exp);
            return id;
        }

        public void RemoveDiscountPolicy(int disId, int memberId)
        {
            //TODO check if permission alows to handle discounts
            string permissionError = CheckAtLeastManagerWithPermission(memberId, Permission.DiscountPolicyManagement);
            if (permissionError != null)
                throw new MarketException("Could not remove discount policy: " + permissionError);

            discountManager.RemoveDiscount(disId);
        }

        // ----------------------------- Purchases policy -----------------------------

        public int AddPurchasePolicy(IPurchasePolicy exp, string descrption, int memberId)
        {
            //TODO check if permission alows to handle discounts
            string permissionError = CheckAtLeastManagerWithPermission(memberId, Permission.purchasePolicyManagement);
            if (permissionError != null)
                throw new MarketException("Could not add purchase policy: " + permissionError);

            int id = purchaseManager.AddPurchasePolicy(descrption, exp);
            return id;
        }

        public void RemovePurchasePolicy(int policyId, int memberId)
        {
            //TODO check if permission alows to handle discounts
            string permissionError = CheckAtLeastManagerWithPermission(memberId, Permission.purchasePolicyManagement);
            if (permissionError != null)
                throw new MarketException("Could not add purchase policy: " + permissionError);

            purchaseManager.RemovePurchasePolicy(policyId);
        }

        public IDictionary<int, string> GetPurchasePolicyDescriptions()
        {
            return purchaseManager.GetDescriptions();
        }

        public IDictionary<int, string> GetDiscountPolicyDescriptions()
        {
            return discountManager.GetDescriptions();
        }

        // ------------------------------ General ------------------------------

        // 4.9
        public void CloseStore(int memberId)
        {
            lock (isOpenMutex)
            {
                string errorMessage = CheckAtLeastFounderPermission(memberId);
                if (errorMessage != null)
                    throw new MarketException("Error in closing the store: " + errorMessage);
                if (!isOpen)
                    throw new MarketException($"{this.name} is allready closed");
                string notificationMessage = $"We regret to inform you that {this.name} has been closed";
                isOpen = false;
                notifyAllStoreOwners(notificationMessage);
                notifyAllStoreManagers(notificationMessage);
            }
        }


        // 1.5, 1.6
        public virtual void notifyAllStoreOwners(string notificationMessage) 
            =>notifyAllMembersWithRole(notificationMessage, Role.Owner);
        public void notifyAllStoreManagers(string notificationMessage)
            => notifyAllMembersWithRole(notificationMessage, Role.Manager);


        private void notifyAllMembersWithRole(string notificationMessage, Role roleAtStore)
        {
            foreach (int memberId in rolesInStore[roleAtStore])
                membersGetter(memberId).Notify(notificationMessage);
        }

        // todo: maybe write tests about thers methods

        private string CheckAtLeastFounderPermission(int memberId)
        {
            if (IsFounder(memberId))
                return null;

            return StoreErrorMessage("The member does not have the permission required in this store: Founder");
        }

        private string CheckAtLeastCoOwnerPermission(int memberId)
        {
            if (CheckAtLeastFounderPermission(memberId) == null)
                return null;
            if (IsCoOwner(memberId))
                return null;
            return StoreErrorMessage("The member does not have the permission required in this store: CoOnwer");
        }

        private string CheckAtLeastManagerWithPermission(int memberId, Permission permission)
        {
            if (CheckAtLeastCoOwnerPermission(memberId) == null)
                return null;
            if (IsManagerWithPermission(memberId, permission))
                return null;
            return StoreErrorMessage("The member is not a manager with the permission " + permission.ToString());
        }

        private string CheckAtLeastMemberPermission(int memberId)
        {
            if (IsMember(memberId))
                return null;
            return StoreErrorMessage("The id: " + memberId + " is not of a member");
        }

        private bool IsMember(int memberId) // should be private
        {
            return membersGetter(memberId) != null;
        }

        private string StoreErrorMessage(string errorMessage)
        {
            return errorMessage + " in the store: " + name; 
        }

        public bool ContainProductInStock(int productId)
        => products.ContainsKey(productId);
        public virtual Product SearchProductByProductId(int productId)
        => products.ContainsKey(productId) ? products[productId] : null;
        public List<Purchase> findPurchasesByDate(DateTime date)
        => purchaseHistory.Where(p => p.purchaseDate == date).ToList();

    }
}