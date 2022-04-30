using MarketBackend.BusinessLayer.Buyers.Members;
using System;
using System.Collections.Concurrent;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class Store
    {
        public string name { get; }
        public Member founder { get; }
        public Hierarchy<Member> appointmentsHierarchy { get; }
        public StorePolicy policy { get; }
        public IDictionary<int,Product> products { get; }
        
        private IList<Purchase> purchaseHistory;
        private IDictionary<int, IList<Permission>> managersPermissions;
        private IDictionary<Role, IList<int>> rolesInStore;
        private Func<int, Member> membersGetter;

        private static int productIdCounter = 0; // the next store id
        private static Mutex productIdCounterMutex = new Mutex();

        private const int timeoutMilis = 2000; // time for wating for the rw lock in the next line, after which it throws an exception
        private ReaderWriterLock rolesAndPermissionsLock;

        // cc 5
        // cc 6
        public Store(string storeName, Member founder, Func<int, Member> membersGetter)
	    {
            this.name = storeName;
            this.founder = founder;
            this.appointmentsHierarchy = new Hierarchy<Member>(founder);
            this.purchaseHistory = new SynchronizedCollection<Purchase>();
            this.policy = new StorePolicy();
            this.products = new ConcurrentDictionary<int,Product>();
            this.managersPermissions = new ConcurrentDictionary<int, IList<Permission>>();
            initializeRolesInStore();
            this.membersGetter = membersGetter;

            this.rolesAndPermissionsLock = new ReaderWriterLock(); // no need to acquire it here (probably) because constructor is of one thread
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
        private static int GenerateProductId()
        {
            productIdCounterMutex.WaitOne();

            int result = productIdCounter;
            productIdCounter++;

            productIdCounterMutex.ReleaseMutex();

            return result;
        }
        private void EnforceAtLeastCoOwnerPermission(int memberId, string message) {
            string permissionError = CheckAtLeastCoOwnerPermission(memberId);
            if (permissionError != null)
                throw new MarketException(StoreErrorMessage(message + permissionError));
        }
        
        // r.4.1
        public int AddNewProduct(int memberId, string productName, double pricePerUnit, string category) {
            // we allow this only to coOwners
            EnforceAtLeastCoOwnerPermission(memberId,"Could not add a new product: ");
            int id = GenerateProductId();
            products.Add(id, new Product(productName, pricePerUnit, category));
            return id;
        }
        // r.4.1
        public void AddProductToInventory(int memberId, int productId, int amount) {
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
        public void DecreaseProductAmountFromInventory(int memberId, int productId, int amount)
        {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not take from inventory: ");
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not take from inventory: there isn't such a product with product id: {productId}"));
            try {
                products[productId].RemoveFromInventory(amount);
            }
            catch(MarketException mEx) {
                throw new MarketException(StoreErrorMessage($"Could not take from inventory: {mEx.Message}"));
            }
        }
        // r.4.2
        public void AddPurchaseOption(int memberId, PurchaseOption purchaseOption)//Add to store
        {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not add purchase option: ");
            policy.AddPurchaseOption(purchaseOption); 
        }
       
        // r.4.2
        public void AddProductPurchaseOption(int memberId, int productId, PurchaseOption purchaseOption)//Add to product in the store 
        {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not add purchase option for the product: ");
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not add purchase option for the product: there isn't such a product with product id: {productId}"));
            if (!policy.ContainsPurchaseOption(purchaseOption))
                throw new MarketException(StoreErrorMessage($"Could not add purchase option for the product: the store itself does not support such purchase options"));
            products[productId].AddPurchaseOption(purchaseOption);
        }
        // r.4.2
        public void SetMinAmountPerProduct(int memberId, int productId, int newMinAmount)//Add to store
        {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not set minimum amount for product: ");
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not set minimum amount for product: there isn't such a product with product id: {productId}"));
            policy.SetMinAmountPerProduct(productId, newMinAmount);
        }

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
        public void AddProductReview(int memberId, int productId, string review) {
            string permissionError = CheckAtLeastMemberPermission(memberId);
            if (permissionError !=null)
                throw new MarketException("Could not add review: "+permissionError);
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not add review: there isn't such a product with product id: {productId}"));
            products[productId].AddProductReview(membersGetter(memberId).Username,review);
        }
        // 6.4, 4.13
        public void AddPurchaseRecord(int memberId, DateTime purchaseDate, double PurchasePrice, string purchaseDescription) 
        {
            EnforceAtLeastCoOwnerPermission(memberId, "Could not add purchase option for the product: ");
            purchaseHistory.Add(new Purchase(purchaseDate, PurchasePrice, purchaseDescription));
        }
        // 6.4, 4.13
        public IList<Purchase> GetPurchaseHistory(int memberId)
        {
            EnforceAtLeastCoOwnerPermission(memberId, "could not get the purchase history: ");
            return purchaseHistory;
        }
        // r.3.3
        public IList<string> GetProductReviews(int productId)
        {
            if (!products.ContainsKey(productId))
                throw new MarketException(StoreErrorMessage($"Could not get reviews: there isn't such a product with product id: {productId}"));
            return products[productId].reviews;
        }
        // r.3.3
        public void AddDiscountForAmountPolicy(int memeberId, int amount, double discount)
        {
            EnforceAtLeastCoOwnerPermission(memeberId, "Could not add store discount for a certain amount: ");
            policy.AddDiscountAmountPolicy(amount, discount);
        }
        // r.3.3
        //recieves <productId, productAmount> and calculates the total to pay, consideroing all the restroctions
        public double GetTotalBagCost(IDictionary<int,int> productsAmounts) 
        {
            foreach (int productId in productsAmounts.Keys)
            {
                if (!products.ContainsKey(productId))
                    throw new MarketException(StoreErrorMessage($"Could not calculate bag total to pay: there isn't such a product with product id: {productId}"));
                int amountPerProduct = policy.GetMinAmountPerProduct(productId);
                if (productsAmounts[productId] < amountPerProduct)
                    throw new MarketException(StoreErrorMessage($"Could not calculate bag total to pay:  {products[productId].name} can be bought only in a set of {amountPerProduct} or more"));
            }
            double productsTotalPrices = productsAmounts.Keys.Select(productId => productsAmounts[productId]*products[productId].getUnitPriceWithDiscount()).ToList().Sum();
            double amountDiscount = policy.GetDiscountForAmount(productsAmounts.Values.Sum());
            return productsTotalPrices * (1 - amountDiscount);
        }
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
            if (IsManager(newCoOwnerMemberId))
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException(StoreErrorMessage("The member is already a Manager"));
            }
            if (!IsMember(newCoOwnerMemberId))
            {
                rolesAndPermissionsLock.ReleaseWriterLock();
                throw new MarketException("The requested new CoOwner is not a member");
            }

            rolesInStore[Role.Owner].Add(newCoOwnerMemberId);

            appointmentsHierarchy.AddToHierarchy(membersGetter(requestingMemberId), membersGetter(newCoOwnerMemberId));
            // todo: add tests checking this field has been changed
            
            rolesAndPermissionsLock.ReleaseWriterLock();
            // todo: check that this mutex is synchronizing all these things okay
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

            appointmentsHierarchy.AddToHierarchy(membersGetter(requestingMemberId), membersGetter(newCoManagerMemberId));
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

            // no need to aquire lock because the second action does not depend on the first

            return new List<int>(rolesInStore[role]); 
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

            IList<Permission> result = new List<Permission>(managersPermissions[managerMemberId]);
            rolesAndPermissionsLock.ReleaseReaderLock();
            return result; 
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

        // ------------------------------ General ------------------------------

        // 4.9
        public void CloseStore(int memberId)
        {
            // todo: implement
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
        public Product SearchProductByProductId(int productId)
        => products.ContainsKey(productId) ? products[productId] : null;
        public List<Purchase> findPurchasesByDate(DateTime date)
        => purchaseHistory.Where(p => p.purchaseDate == date).ToList();

    }
}