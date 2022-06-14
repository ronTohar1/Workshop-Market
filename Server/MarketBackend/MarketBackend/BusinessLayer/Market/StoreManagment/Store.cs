using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts;
using System;
using System.Collections.Concurrent;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.DataLayer.DataManagers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataDTOs.Market;

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


        private const int timeoutMilis = 3000; // time for wating for the rw lock in the next line, after which it throws an exception
        private ReaderWriterLock rolesAndPermissionsLock;

        private IDictionary<int, IDictionary<Product, int>> transactions;
        private ConcurrentDictionary<int, Mutex> productsMutex;
        private Mutex transactionIdMutex;

        public StoreDiscountPolicyManager discountManager { get; }
        public virtual StorePurchasePolicyManager purchaseManager { get; }

        public IDictionary<int, Bid> bids { get; }
        private Mutex approvebidLock;

        int id; 

        private StoreDataManager storeDataManager; // r S 8

        // cc 5
        // cc 6
        public Store(string storeName, Member founder, Func<int, Member> membersGetter)
            : this(
                  storeName,
                  founder,
                  true,
                  new Hierarchy<int>(founder.Id, (dataAppointmentsNode, value) => dataAppointmentsNode.MemberId = value),
                  new ConcurrentDictionary<int, Product>(),
                  new SynchronizedCollection<Purchase>(),
                  new ConcurrentDictionary<int, IList<Permission>>(),
                  membersGetter,
                  new StoreDiscountPolicyManager(),
                  new StorePurchasePolicyManager(),
                  new ConcurrentDictionary<int, Bid>())
        {

        }

        private Store(string name, Member founder, bool isOpen, Hierarchy<int> appointmentsHierarchy,
            IDictionary<int, Product> products, IList<Purchase> purchaseHistory,
            IDictionary<int, IList<Permission>> managersPermissions, Func<int, Member> membersGetter,
            StoreDiscountPolicyManager discountManager, StorePurchasePolicyManager purchaseManager,
            IDictionary<int, Bid> bids, IDictionary<Role, IList<int>> rolesInStore = null)
        {
            this.name = name;
            this.founder = founder;
            this.isOpen = isOpen;
            this.appointmentsHierarchy = appointmentsHierarchy;
            this.products = products;
            this.purchaseHistory = purchaseHistory;
            this.managersPermissions = managersPermissions;
            this.membersGetter = membersGetter;
            this.discountManager = discountManager;
            this.purchaseManager = purchaseManager;
            this.bids = bids;

            if (rolesInStore == null)
                initializeRolesInStore();
            else
                this.rolesInStore = rolesInStore;

            this.isOpenMutex = new Mutex();

            //Transactions
            this.transactions = new Dictionary<int, IDictionary<Product, int>>();
            this.productsMutex = new();
            this.transactionIdMutex = new Mutex();

            //bids
            this.approvebidLock = new Mutex(false);

            this.rolesAndPermissionsLock = new ReaderWriterLock(); // no need to acquire it here (probably) because constructor is of one thread

            this.storeDataManager = StoreDataManager.GetInstance();
        }

        // r S 8
        public static Store DataStoreToStore(DataStore dataStore, Func<int, Member> membersGetter)
        {
            Member founder = membersGetter(dataStore.Founder.Id);

            Hierarchy<int> appointmentsHierarchy = Hierarchy<int>.DataHierarchyToHierarchy(dataStore.Appointments);

            IDictionary<int, Product> products = new ConcurrentDictionary<int, Product>();
            foreach (DataProduct dataProduct in dataStore.Products)
            {
                products.Add(dataProduct.Id, Product.DataProductToProduct(dataProduct));
            }

            IList<Purchase> purchaseHistory = dataStore.PurchaseHistory
                .Select(dataPurchase => Purchase.DataPurchaseToPurchase(dataPurchase)).ToList();

            IDictionary<Role, IList<int>> rolesInStore = GetRolesInStoresWithRoles();
            IDictionary<int, IList<Permission>> managersPermissions = new ConcurrentDictionary<int, IList<Permission>>();
            int memberId;
            foreach (DataStoreMemberRoles dataPermissions in dataStore.MembersPermissions)
            {
                memberId = dataPermissions.MemberId;
                rolesInStore[dataPermissions.Role].Add(memberId);
                if (dataPermissions.Role == Role.Manager)
                {
                    managersPermissions.Add(memberId,
                        dataPermissions.ManagerPermissions.Select(dataPermission => (Permission)dataPermission.Permission).ToList());
                }
            }

            StoreDiscountPolicyManager discountManager = StoreDiscountPolicyManager.DataSDPMToSDPM(dataStore.DiscountManager);

            StorePurchasePolicyManager purchaseManager = StorePurchasePolicyManager.DataSPPMToSPPM(dataStore.PurchaseManager);

            IDictionary<int, Bid> bids = new ConcurrentDictionary<int, Bid>();
            foreach (DataBid dataBid in dataStore.Bids)
            {
                bids[dataBid.Id] = Bid.DataBidToBid(dataBid, dataStore.Id);
            }

            return new Store(dataStore.Name, founder, dataStore.IsOpen, appointmentsHierarchy, products, purchaseHistory,
                managersPermissions, membersGetter, discountManager, purchaseManager, bids, rolesInStore);
        }

        // todo: implement function after adding the add to store's fields functions 

        // without the store id 
        public DataStore ToNewDataStore()
        {
            DataStore result = new DataStore()
            {
                Name = name,
                Founder = MemberDataManager.GetInstance().Find(founder.Id),
                IsOpen = isOpen,
                Products = products.Values.Select(product => product.ToNewDataProduct()).ToList(),
                PurchaseHistory = purchaseHistory.Select(purchase => purchase.ToNewDataPurchase(null)).ToList(),
                MembersPermissions = GetNewDataStoreMemberRoles(null),
                Appointments = appointmentsHierarchy.ToNewDataAppointmentsNode(),
                DiscountManager = discountManager.SDPMToDSDPM(), 
                PurchaseManager = purchaseManager.SPPMToDataDSPPM(), 
                Bids = bids.Values.Select(bid => bid.ToNewDataBid()).ToList()
            };
            
            foreach(DataPurchase dataPurchase in result.PurchaseHistory)
            {
                dataPurchase.Store = result; 
            }

            foreach(DataStoreMemberRoles memberPermissions in result.MembersPermissions)
            {
                memberPermissions.Store = result;
            }

            return result; 
        }

        private IList<DataStoreMemberRoles> GetNewDataStoreMemberRoles(DataStore dataStore)
        {
            IList<DataStoreMemberRoles> result = new List<DataStoreMemberRoles>();
            DataStoreMemberRoles dataRole;
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                foreach (int memberId in rolesInStore[role])
                {
                    dataRole = new DataStoreMemberRoles()
                    {
                        Store = dataStore,
                        MemberId = memberId,
                        Role = role,
                        ManagerPermissions = null
                    };

                    if (role == Role.Manager)
                    {
                        dataRole.ManagerPermissions = managersPermissions[memberId].Select(
                            permission => new DataManagerPermission() { Permission = permission }
                            ).ToList();
                    }

                    result.Add(dataRole); 
                }
            }
            return result; 
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
                    product.AddToInventory(amount, new Action(() => storeDataManager.Save()));
                }
                transactions.Remove(transactionId);
            }
            else
                return false;
            return true;

        }

        // Reserving products.
        // If successful then assigning the transactionId, else returns informative string why failed.
        public string? ReserveProducts(int buyerId, IDictionary<int, int> productsAmounts, out int transactionId)
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

        private void RevertTransaction(int transaction)
        {
            foreach (Product p in transactions[transaction].Keys)
                p.AddToInventory(transactions[transaction][p], new Action(() => storeDataManager.Save()));
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
                    return "Could not buy product: " + products[productId].name + ". An unexpected error has occurd : " + e.Message;
                }
            }
            return null;
        }

        private int CreateNewTransaction()
        {
            lock (transactionIdMutex)
            {
                int maxId = transactions.Count == 0 ? 0 : transactions.Keys.Max();
                transactions.Add(maxId + 1, new Dictionary<Product, int>());
                return maxId + 1;
            }
        }

        private static IDictionary<Role, IList<int>> GetRolesInStoresWithRoles()
        {
            IDictionary<Role, IList<int>> rolesInStore = new ConcurrentDictionary<Role, IList<int>>();
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                rolesInStore.Add(role, new SynchronizedCollection<int>());
            }
            return rolesInStore;
        }

        private void initializeRolesInStore()
        {
            // need to be called from constructor or with acquiring the lock 
            // saving founder as a coOnwer as well
            if (founder == null)
                throw new ArgumentNullException("Initializing roles in stores should happen after founder is initialized");
            this.rolesInStore = GetRolesInStoresWithRoles();
            this.rolesInStore[Role.Owner].Add(founder.Id);
        }

        public virtual string GetName()
        {
            return name;
        }

        // ------------------------------ Products and Policy ------------------------------
        private void EnforceAtLeastCoOwnerPermission(int memberId, string message)
        {
            string permissionError = CheckAtLeastCoOwnerPermission(memberId);
            if (permissionError != null)
                throw new MarketException(StoreErrorMessage(message + permissionError));
        }

        // r.4.1
        public int AddNewProduct(int memberId, string productName, double pricePerUnit, string category) {
            string permError = CheckAtLeastManagerWithPermission(memberId, Permission.ManageInventory);
            if (permError != null)
                throw new MarketException("Could not add product: " + permError);
            if (pricePerUnit <= 0)
                throw new MarketException("Cannot add product with price smaller or equal to 0!");
            Product newProduct = new Product(productName, pricePerUnit, category);
            DataProduct dataProduct = newProduct.ToNewDataProduct();
            storeDataManager.Update(id, store => store.Products.Add(dataProduct)); 

            storeDataManager.Save(); 

            products.Add(newProduct.id, newProduct);
            return newProduct.id;
        }
        // r.4.1
        public virtual void AddProductToInventory(int memberId, int productId, int amount) {
            string permError = CheckAtLeastManagerWithPermission(memberId, Permission.ManageInventory);
            if (permError != null)
                throw new MarketException("Could not change inventory: " + permError);
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not add to inventory:\n there isn't such a product with product id {productId}"));
            products[productId].AddToInventory(amount, () => storeDataManager.Save());
        }
        // r.4.1
        public void RemoveProduct(int memberId, int productId) {
            string permError = CheckAtLeastManagerWithPermission(memberId, Permission.ManageInventory);
            if (permError != null)
                throw new MarketException("Could not remove product: " + permError);
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not remove a product:\n there isn't such a product with product id {productId}"));
            
            Product product = products[productId];

            product.RemoveData();
            storeDataManager.Save(); 
            
            products.Remove(productId);
        }

        // r.4.1
        // c.9
        public virtual void DecreaseProductAmountFromInventory(int memberId, int productId, int amount)
        {
            string permError = CheckAtLeastManagerWithPermission(memberId, Permission.ManageInventory);
            if (permError != null)
                throw new MarketException("Could not change inventory: " + permError);
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not take from inventory:\n there isn't such a product with product id {productId}"));
            try
            {
                products[productId].RemoveFromInventory(amount, () => storeDataManager.Save());
            }
            catch (MarketException mEx)
            {
                throw new MarketException(StoreErrorMessage($"Could not take from inventory: {mEx.Message}"));
            }
        }

        public IList<Product> SerachProducts(ProductsSearchFilter filter)
        => products.Values.Where(p => filter.FilterProduct(p)).ToList();


        // r.4.1
        public void SetProductPrice(int memberId, int productId, double productPrice)
        {
            string permError = CheckAtLeastManagerWithPermission(memberId, Permission.ManageInventory);
            if (permError != null)
                throw new MarketException("Could not set the product price per unit: " + permError);
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not set the product price per unit:\n there isn't such a product with product id {productId}"));
            products[productId].SetProductPriceByUnit(productPrice, () => storeDataManager.Save());
        }
        // r.4.1
        public void SetProductDiscountPercentage(int memberId, int productId, double discountPercentage)
        {
            string permError = CheckAtLeastManagerWithPermission(memberId, Permission.ManageInventory);
            if (permError != null)
                throw new MarketException("Could not set the product's discount percentage: " + permError);
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not set the product's discount percentage:\n there isn't such a product with product id: {productId}"));
            products[productId].SetProductDiscountPercentage(discountPercentage, () => storeDataManager.Save());
        }
        // r.4.1
        public void SetProductCategory(int memberId, int productId, string category)
        {
            string permError = CheckAtLeastManagerWithPermission(memberId, Permission.ManageInventory);
            if (permError != null)
                throw new MarketException("Could not set the product's category: " + permError);
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not set the product's category:\n there isn't such a product with product id: {productId}"));
            products[productId].SetProductCategory(category, () => storeDataManager.Save());
        }
        // r.3.3
        // r.1.5, 1.6
        public void AddProductReview(int memberId, int productId, string review)
        {
            string permissionError = CheckAtLeastMemberPermission(memberId);
            if (permissionError != null)
                throw new MarketException("Could not add review: " + permissionError);
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not add review: there isn't such a product with product id: {productId}"));
            string notification = $"The member with id: {memberId} has written a new review of a product woth id: {productId} at {this.name}";
            products[productId].AddProductReview(memberId, review, () =>
            {
                DataNotifyAllStoreOwners(notification); 
                storeDataManager.Save();
            });
            notifyAllStoreOwnersNoSave(notification);
        }
        // 6.4, 4.13
        public virtual void AddPurchaseRecord(int memberId, Purchase purchase)
        {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not add purchase option for the product: ");
            purchaseHistory.Add(purchase);
        }

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
            string permError = CheckAtLeastManagerWithPermission(memberId, Permission.ManageInventory);
            if (permError != null)
                throw new MarketException("Could not get the purchase history: " + permError);
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
                IDictionary<Member, IList<string>> membersToReviews = new Dictionary<Member, IList<string>>();
                foreach (int memberId in memberIdToReviews.Keys)
                {
                    Member? m = membersGetter(memberId);
                    if (m != null)
                        membersToReviews[m] = memberIdToReviews[memberId];
                }
                return membersToReviews;
            }
        }

        // r.3.3
        //recieves shopping bag and calculates the total to pay, consideroing all the restroctions
        public virtual Tuple<double, double> GetTotalBagCost(ShoppingBag shoppingBag)
        {
            foreach (var product in shoppingBag.ProductsAmounts)
            {
                int productId = product.Key.ProductId;
                int productAmount = product.Value;

                if (!products.ContainsKey(productId))
                    throw new MarketException(StoreErrorMessage($"Could not calculate bag total to pay: there isn't such a product"));
            }
            double productsTotalPrices = shoppingBag.ProductsAmounts.Keys.Sum(prodInBag => products[prodInBag.ProductId].GetPrice() * shoppingBag.ProductsAmounts[prodInBag]);
            double amountDiscount = discountManager.EvaluateDiscountForBag(shoppingBag, this);
            return new(productsTotalPrices, amountDiscount);
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
        public void MakeCoOwner(int requestingMemberId, int newCoOwnerMemberId)
        {
            try
            {
                rolesAndPermissionsLock.AcquireWriterLock(timeoutMilis);
                string permissionError = CheckAtLeastCoOwnerPermission(requestingMemberId);
                if (permissionError != null)
                {
                    throw new MarketException("Could not make owner: " + permissionError);
                }

                if (IsCoOwner(newCoOwnerMemberId))
                {
                    throw new MarketException(StoreErrorMessage("The member is already a CoOwner"));
                }
                if (!IsMember(newCoOwnerMemberId))
                {
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
                        throw new MarketException("The requesting member is not the one that appointed the member to a manager, so can not appoint it to be a coOwner");
                        // todo: also add tests for when the appointing coOwner is not the one that appointed the member of newCoOwnerId to be a manager
                    }

                    storeDataManager.Update(id, store =>
                    {
                        DataStoreMemberRoles dataRole = store.MembersPermissions.First(dataPermission => dataPermission.MemberId == newCoOwnerMemberId);
                        dataRole.ManagerPermissions = null;
                        dataRole.Role = Role.Owner;
                    });
                    storeDataManager.Save();

                    managersPermissions.Remove(newCoOwnerMemberId);
                    rolesInStore[Role.Manager].Remove(newCoOwnerMemberId);
                }
                else
                {
                    appointmentsHierarchy.AddToHierarchy(requestingMemberId, newCoOwnerMemberId, () =>
                    {
                        DataStore dataStore = storeDataManager.Find(id);
                        DataStoreMemberRoles dataRole = new DataStoreMemberRoles()
                        {
                            Role = Role.Owner,
                            ManagerPermissions = null,
                            Store = dataStore,
                            MemberId = newCoOwnerMemberId
                        };
                        dataStore.MembersPermissions.Add(dataRole);
                        storeDataManager.Save();
                    });
                    // todo: add tests checking this field has been changed (and for what happens when newCoOwnerId is of a manager)
                }
                rolesInStore[Role.Owner].Add(newCoOwnerMemberId);
            }
            finally
            {
                if (rolesAndPermissionsLock.IsWriterLockHeld)
                    rolesAndPermissionsLock.ReleaseWriterLock();
            }
            // todo: check that this mutex is synchronizing all these things okay
        }

        // r 4.5
        public void RemoveCoOwner(int requestingMemberId, int toRemoveCoOwnerMemberId)
        {
            try
            {
                rolesAndPermissionsLock.AcquireWriterLock(timeoutMilis);
                string permissionError = CheckAtLeastCoOwnerPermission(requestingMemberId);
                if (permissionError != null)
                {
                    throw new MarketException("Could not remove co owner: " + permissionError);
                }

                permissionError = CheckAtLeastCoOwnerPermission(toRemoveCoOwnerMemberId);
                if (permissionError != null)
                {
                    throw new MarketException("Could not remove co owner: " + permissionError);
                }

                string notification = $"We regeret to inform you that you've lost your position at {this.name}";

                IList<int> memberIdsToRemove = appointmentsHierarchy.GetHierarchyValueList();
                Hierarchy<int> removedBrance = appointmentsHierarchy.RemoveFromHierarchy(requestingMemberId, toRemoveCoOwnerMemberId, () =>
                {
                    DataStore dataStore = storeDataManager.Find(id);

                    // remove roles in database
                    // 
                    foreach (int memberToRemoveId in memberIdsToRemove)
                    {
                        IList<DataStoreMemberRoles> rolesToRemove = dataStore.MembersPermissions.Where(dataRole => dataRole.MemberId == memberToRemoveId).ToList();
                        foreach (DataStoreMemberRoles roleToRemove in rolesToRemove)
                        {
                            StoreMemberRolesDataManager.GetInstance().Remove(roleToRemove.Id);
                        }

                        // notification in database 

                        membersGetter(memberToRemoveId).DataNotify(notification);
                    }

                });
                RemovedByOwnerBranchUpdate(removedBrance, notification);
            }
            finally
            {
                if (rolesAndPermissionsLock.IsWriterLockHeld)
                    rolesAndPermissionsLock.ReleaseWriterLock();
            }
        }
        private void RemovedByOwnerBranchUpdate(Hierarchy<int> removedBranch, string notification)
        {
            if (removedBranch == null)
                return;
            int memberId = removedBranch.value;
            rolesInStore[Role.Owner].Remove(memberId);//doesn't do anything if not in collection
            rolesInStore[Role.Manager].Remove(memberId);
            membersGetter(memberId).NotifyNoSave(notification);
            foreach (Hierarchy<int> child in removedBranch.children) 
            {
                RemovedByOwnerBranchUpdate(child, notification);
            }
        }

        // cc 3
        // r 4.6, r 5
        public void MakeManager(int requestingMemberId, int newCoManagerMemberId)
        {
            try
            {
                rolesAndPermissionsLock.AcquireWriterLock(timeoutMilis);
                string permissionError = CheckAtLeastManagerWithPermission(requestingMemberId, Permission.MakeCoManager);
                if (permissionError != null)
                {
                    throw new MarketException("Could not make manager: " + permissionError);
                }
                if (IsCoOwner(newCoManagerMemberId))
                {
                    throw new MarketException(StoreErrorMessage("The member is already a CoOwner"));
                }
                if (IsManager(newCoManagerMemberId))
                {
                    throw new MarketException(StoreErrorMessage("The member is already a Manager"));
                }
                if (!IsMember(newCoManagerMemberId))
                {
                    throw new MarketException("The requested new CoOwner is not a member");
                }

                IList<Permission> permissions = DefualtManagerPermissions(); 

                appointmentsHierarchy.AddToHierarchy(requestingMemberId, newCoManagerMemberId, () =>
                {
                    DataStore dataStore = storeDataManager.Find(id);
                    DataStoreMemberRoles dataRole = new DataStoreMemberRoles()
                    {
                        Role = Role.Manager,
                        ManagerPermissions = permissions.Select(permission => new DataManagerPermission { Permission = permission }).ToList(),
                        Store = dataStore,
                        MemberId = newCoManagerMemberId
                    };
                    dataStore.MembersPermissions.Add(dataRole);
                    storeDataManager.Save();
                });


                rolesInStore[Role.Manager].Add(newCoManagerMemberId);

                managersPermissions[newCoManagerMemberId] = permissions; 

                // todo: add tests checking this field has been changed

            }
            finally
            {
                if (rolesAndPermissionsLock.IsWriterLockHeld)
                    rolesAndPermissionsLock.ReleaseWriterLock();
            }
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
        public void ChangeManagerPermissions(int requestingMemberId, int managerMemberId, IList<Permission> newPermissions)
        {
            try
            {
                rolesAndPermissionsLock.AcquireWriterLock(timeoutMilis);
                // we allow this only to coOwners
                string permissionError = CheckAtLeastCoOwnerPermission(requestingMemberId);
            if (permissionError != null)
            {
                throw new MarketException("Could not change manager permissions: " + permissionError);
            }

            if (!IsManager(managerMemberId))
            {
                throw new MarketException(StoreErrorMessage("The id: " + managerMemberId + " is not of a managaer"));
            }

            storeDataManager.Update(id, store => store.MembersPermissions
            .FirstOrDefault(memberPermission => memberPermission.MemberId == managerMemberId, null)
            .ManagerPermissions = newPermissions.Select(permission =>
                new DataManagerPermission() { Permission = permission }).ToList()
            );
            storeDataManager.Save();

            managersPermissions[managerMemberId] = newPermissions;

            }
           
            finally
            {
                if (rolesAndPermissionsLock.IsWriterLockHeld)
                    rolesAndPermissionsLock.ReleaseWriterLock();
            }

            // todo: check that this mutex is synchronizing all these things okay
        }

        // r 4.11, r 5
        public virtual IList<int> GetMembersInRole(int memberId, Role role)
        {
            string permissionError = CheckAtLeastManagerWithPermission(memberId, Permission.RecieiveRolesInfo);
            if (permissionError != null)
                throw new MarketException("Error getting the members in role: " + role.ToString() + ".\n " + permissionError);
            return GetMembersInRoleNoPermissionsCheck(role);
        }

        public virtual IList<int> GetMembersInRoleNoPermissionsCheck(Role role)
        {
            lock (isOpenMutex)
            {
                if (!isOpen)
                    throw new MarketException($"Could not check members in role because the store: '{this.name}' is closed");

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
                throw new MarketException("Error getting founder: \n" + permissionError);

            // no need to aquire lock because the second action does not depend on the first

            return founder;
        }

        // r 4.11, r 5
        public IList<Permission> GetManagerPermissions(int requestingMemberId, int managerMemberId)
        {
            string permissionError = CheckAtLeastManagerWithPermission(requestingMemberId, Permission.RecieiveRolesInfo);
            if (permissionError != null)
                throw new MarketException("Error getting manager permissions: \n" + permissionError);

            try
            {
                rolesAndPermissionsLock.AcquireReaderLock(timeoutMilis);
            if (!IsManager(managerMemberId))
            {
                throw new MarketException("The requested member is not a manager, so its permissions cant retrieved");
            }
            lock (isOpenMutex)
            {
                if (!isOpen)
                    throw new MarketException($"Cant get purchase history because the store '{this.name}' is closed");

                IList<Permission> result = new List<Permission>(managersPermissions[managerMemberId]);
                return result;
            }

            }
            
            finally
            {
                if (rolesAndPermissionsLock.IsReaderLockHeld)
                    rolesAndPermissionsLock.ReleaseReaderLock();
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
            try
            {
                rolesAndPermissionsLock.AcquireReaderLock(timeoutMilis);
                if (!IsManager(managerId))
                {
                    throw new ArgumentException(StoreErrorMessage("User with id " + managerId + " is not a managaer"));

                }
                bool result = managersPermissions[managerId] != null && managersPermissions[managerId].Contains(permission);
                return result;
            }
            finally
            {
                if (rolesAndPermissionsLock.IsReaderLockHeld)
                    rolesAndPermissionsLock.ReleaseReaderLock();
            }


        }

        private bool IsManagerWithPermission(int memberId, Permission permission)
        {
            try
            {
                rolesAndPermissionsLock.AcquireReaderLock(timeoutMilis);
                bool result = IsManager(memberId) && HasPermission(memberId, permission);
                return result;
            }
            finally
            {
                if (rolesAndPermissionsLock.IsReaderLockHeld)
                    rolesAndPermissionsLock.ReleaseReaderLock();
            }

        }

        // ----------------------------- Discounts policy -----------------------------

        public int AddDiscountPolicy(IExpression exp, string descrption, int memberId)
        {
            //TODO check if permission alows to handle discounts
            string permissionError = CheckAtLeastManagerWithPermission(memberId, Permission.DiscountPolicyManagement);
            if (permissionError != null)
                throw new MarketException("Could not add discount policy: \n" + permissionError);

            int id = discountManager.AddDiscount(descrption, exp);
            return id;
        }

        public void RemoveDiscountPolicy(int disId, int memberId)
        {
            //TODO check if permission alows to handle discounts
            string permissionError = CheckAtLeastManagerWithPermission(memberId, Permission.DiscountPolicyManagement);
            if (permissionError != null)
                throw new MarketException("Could not remove discount policy: \n" + permissionError);

            discountManager.RemoveDiscount(disId);
        }

        // ----------------------------- Purchases policy -----------------------------

        public int AddPurchasePolicy(IPurchasePolicy exp, string descrption, int memberId)
        {
            //TODO check if permission alows to handle discounts
            string permissionError = CheckAtLeastManagerWithPermission(memberId, Permission.purchasePolicyManagement);
            if (permissionError != null)
                throw new MarketException("Could not add purchase policy: \n" + permissionError);

            int id = purchaseManager.AddPurchasePolicy(descrption, exp);
            return id;
        }

        public void RemovePurchasePolicy(int policyId, int memberId)
        {
            //TODO check if permission alows to handle discounts
            string permissionError = CheckAtLeastManagerWithPermission(memberId, Permission.purchasePolicyManagement);
            if (permissionError != null)
                throw new MarketException("Could not add purchase policy: \n" + permissionError);

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

        // ------------------------------- Bids --------------------------------

        //every member can add a bid
        public int AddBid(int productId, int memberId, int storeId, double bidPrice)
        {
            Bid bid = new Bid(productId, memberId, storeId, bidPrice);
            DataBid dataBid = bid.ToNewDataBid();
            storeDataManager.Update(id, store => store.Bids.Add(dataBid));

            string storeOwnersNotification = $"A bid has been made for the product {products[productId].name} for the price of {bidPrice}";
            string membersWithRoleAndPermissionNotification = $"A bid has been made for the product {products[productId].name} for the price of {bidPrice}"; 

            DataNotifyAllStoreOwners(storeOwnersNotification);
            DataNotifyAllMembersWithRoleAndPermission(membersWithRoleAndPermissionNotification, Role.Manager, Permission.handlingBids);

            storeDataManager.Save(); 

            bids.Add(bid.id, bid);
            notifyAllStoreOwners(storeOwnersNotification);
            notifyAllMembersWithRoleAndPermission(membersWithRoleAndPermissionNotification, Role.Manager, Permission.handlingBids);

            return bid.id;
        }

        public bool CheckAllApproved(Bid bid)
        {
            approvebidLock.WaitOne();
            IList<int> approved = bid.aprovingIds;

            IList<int> owners = GetMembersInRoleNoPermissionsCheck(Role.Owner);
            foreach (int i in owners)
                if (!approved.Contains(i))
                {
                    approvebidLock.ReleaseMutex();
                    return false;
                }

            IList<int> managers = GetMembersInRoleNoPermissionsCheck(Role.Manager);
            foreach (int i in managers)
            {
                if (IsManagerWithPermission(i, Permission.handlingBids) && !approved.Contains(i))
                {
                    approvebidLock.ReleaseMutex();
                    return false;
                }
            }
            approvebidLock.ReleaseMutex();
            return true;
        }
        // bid actions owners and managers can do
        public void ApproveBid(int memberId, int bidId)
        {
            Bid bid = bids[bidId];
            if (bid.counterOffer)
                throw new MarketException("The bid cannot be approved beacause a counter offer has been made but not been approved by the member");
            string permissionString = CheckAtLeastManagerWithPermission(memberId, Permission.handlingBids);
            if (permissionString != null)
                throw new MarketException(permissionString + " to approve a bid"); 

            approvebidLock.WaitOne();
            try
            {
                Member m = membersGetter.Invoke(bid.memberId);
                string notification = $"The bid you placed for the product {products[bid.productId].name} was approved for the cost of {bid.bid}";

                bid.approveBid(memberId, () =>
                {
                    if (CheckAllApproved(bid))
                        m.DataNotify(notification);

                    storeDataManager.Save();
                });
                if (CheckAllApproved(bid))
                    m.Notify(notification);
            }
            catch (Exception exception)
            {
                throw exception; 
            }
            finally
            {
                approvebidLock.ReleaseMutex();
            }
        }

        public void DenyBid(int memberId, int bidId)
        {
            Bid bid = bids[bidId];
            if (IsCoOwner(memberId) || IsManagerWithPermission(memberId, Permission.handlingBids))
            {
                Member m = membersGetter.Invoke(bid.memberId);
                string notification = $"The bid you placed for the product {products[bid.productId].name} was denied for the cost of {bid.bid}"; 

                BidDataManager.GetInstance().Remove(bidId);
                m.DataNotify(notification);
                storeDataManager.Save(); 

                bids.Remove(bidId);
                m.NotifyNoSave(notification);
            }
        }

        public void MakeCounterOffer(int memberId, int bidId, double offer)
        {
            Bid bid = bids[bidId];
            if (bid.counterOffer)
                throw new MarketException("The bid cannot be approved beacause a counter offer has been made but not been approved by the member");
            if (IsCoOwner(memberId) || IsManagerWithPermission(memberId, Permission.handlingBids))
            {
                bid.CounterOffer(offer);
            }
        }

        public IList<int> GetApproveForBid(int memberId, int bidId)
        {
            Bid bid = bids[bidId];
            if (IsCoOwner(memberId) || IsManagerWithPermission(memberId, Permission.handlingBids))
                return bid.aprovingIds;
            return null;
        }

        public Bid GetBid(int bidId)
        {
            return bids[bidId];
        }

        // actions for a member on his own bid
        public void RemoveBid(int memberId, int bidId)
        {
            if (!bids.ContainsKey(bidId))
            {
                throw new MarketException("This bid does not exist in the system"); 
            }
            if (bids[bidId].memberId != memberId)
            {
                throw new MarketException("This bid belongs to another member"); 
            }
            BidDataManager.GetInstance().Remove(bidId);
            storeDataManager.Save(); 

            bids.Remove(bidId);
        }

        public void ApproveCounterOffer(int memberId, int bidId)
        {
            Bid bid = bids[bidId];
            if (bid.memberId != memberId)
                throw new MarketException("The counter offer cant be approved because it is not your bid!");
            string notification = $"A counter offer on a bid has been made for the product {products[bid.productId].name} for the price of {bid.bid}"; 
            bid.approveCounterOffer(() =>
            {
                DataNotifyAllStoreOwners(notification);
                storeDataManager.Save(); 
            });
            notifyAllStoreOwners(notification);
            notifyAllMembersWithRoleAndPermissionNoSave($"A bid has been made for the product {products[bid.productId].name} for the price of {bid.bid}", Role.Manager, Permission.handlingBids);
        }

        public void DenyCounterOffer(int memberId, int bidId)
        {
            Bid bid = bids[bidId];
            if (bid.memberId != memberId)
                throw new MarketException("The counter offer cant be denied because it is not your bid!");

            bid.DataRemove();
            storeDataManager.Save(); 
            
            bids.Remove(bidId);
        }

        // ------------------------------ Daily profit -------------------------
        public virtual double GetDailyProfit(int memberId)
        {
            string permissionError = CheckAtLeastCoOwnerPermission(memberId);
            if (permissionError != null)
            {
                throw new MarketException("Could not get the store daily profit: " + permissionError);
            }
            return GetDailyProfit();
        }
        public virtual double GetDailyProfit()
        {
            double total = 0;
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            foreach (Purchase p in purchaseHistory)
            {
                DateTime date = p.purchaseDate;
                if ((date.Day == day) && (date.Month == month) && (date.Year == year))
                {
                    total += p.purchasePrice;
                }
            }
            return total;
        }

        // ------------------------------ General ------------------------------

        // 4.9
        public void CloseStore(int memberId, int storeId)
        {
            lock (isOpenMutex)
            {
                string errorMessage = CheckAtLeastFounderPermission(memberId);
                if (errorMessage != null)
                    throw new MarketException("Error in closing the store: " + errorMessage);
                if (!isOpen)
                    throw new MarketException($"{this.name} is allready closed");
                string notificationMessage = $"We regret to inform you that {this.name} has been closed";
                
                storeDataManager.Update(storeId, dataStore => dataStore.IsOpen = false);

                notifyAllStoreOwners(notificationMessage);
                notifyAllStoreManagers(notificationMessage);

                storeDataManager.Save(); 

                isOpen = false;
            }
        }


        // 1.5, 1.6
        public virtual void notifyAllStoreOwners(string notificationMessage) 
            =>notifyAllMembersWithRole(notificationMessage, Role.Owner);
        public virtual void notifyAllStoreOwnersNoSave(string notificationMessage)
            => notifyAllMembersWithRoleNoSave(notificationMessage, Role.Owner);
        public void notifyAllStoreManagers(string notificationMessage)
            => notifyAllMembersWithRole(notificationMessage, Role.Manager);

        private void DataNotifyAllStoreOwners(string notificationMessage)
            => DataNotifyAllMembersWithRole(notificationMessage, Role.Owner);


        private void notifyAllMembersWithRole(string notificationMessage, Role roleAtStore)
        {
            actionAllMembersWithRole(member => member.Notify(notificationMessage), roleAtStore);

        }
        private void DataNotifyAllMembersWithRole(string notificationMessage, Role roleAtStore)
        {
            actionAllMembersWithRole(member => member.DataNotify(notificationMessage), roleAtStore); 
        }
        private void notifyAllMembersWithRoleNoSave(string notificationMessage, Role roleAtStore)
        {
            actionAllMembersWithRole(member => member.NotifyNoSave(notificationMessage), roleAtStore);
        }
        private void actionAllMembersWithRole(Action<Member> action, Role roleAtStore)
        {
            foreach (int memberId in rolesInStore[roleAtStore])
                action(membersGetter(memberId));
        }
        private void notifyAllMembersWithRoleAndPermission(string notificationMessage, Role roleAtStore, Permission permission)
        {
            actionAllMembersWithRoleAndPermission(member => member.Notify(notificationMessage), roleAtStore, permission); 
        }

        private void notifyAllMembersWithRoleAndPermissionNoSave(string notificationMessage, Role roleAtStore, Permission permission)
        {
            actionAllMembersWithRoleAndPermission(member => member.NotifyNoSave(notificationMessage), roleAtStore, permission);
        }

        private void DataNotifyAllMembersWithRoleAndPermission(string notificationMessage, Role roleAtStore, Permission permission)
        {
            actionAllMembersWithRoleAndPermission(member => member.DataNotify(notificationMessage), roleAtStore, permission);
        }

        private void actionAllMembersWithRoleAndPermission(Action<Member> action, Role roleAtStore, Permission permission)
        {
            foreach (int memberId in rolesInStore[roleAtStore])
            {
                if (HasPermission(memberId, permission))
                    action(membersGetter(memberId));
            }
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


        //for tests
        public Store()
        {

        }

    }
}