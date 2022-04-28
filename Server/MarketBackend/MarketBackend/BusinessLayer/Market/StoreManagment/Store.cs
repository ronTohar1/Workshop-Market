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
	    }

        private void initializeRolesInStore()
        {
            if (founder == null)
                throw new ArgumentNullException("Initializing roles in stores should happen after founder is initialized"); 
            this.rolesInStore = new ConcurrentDictionary<Role, IList<int>>();
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                rolesInStore.Add(role, new List<int>());
            }
            this.rolesInStore[Role.Owner].Add(founder.GetId());
        }

        public virtual string GetName()
        {
            return name;
        }

        // ------------------------------ Products ------------------------------
        public int AddNewProduct(int memberId, string productName, double pricePerUnit) {
            return 0;
            // todo: implement
        }
        public void AddProductToInventory(int memberId, int productId, int amount) {
            // todo: implement
        }
        public void RemoveProduct(int memberId, int productId) {
            // todo: implement
        }
        public void DecreaseProductAmountFromInventory(int memberId, int productId, int amount)
        {
            // todo: implement
        }
        public void AddProductPurchasePolicy(int memberId, int productId, PurchaseOption purchaseOption) {
            // todo: implement
        }
        public void SetProductPrice(int memberId, int productId, PurchaseOption purchaseOption)
        {
            // todo: implement
        }
        public void AddProductReview(int memberId, int productId, string review) {
            // todo: implement note that the member Id here is for the memmber info, such as name
        }
        public void GetProductReviews(int productId)
        {
            // todo: implement
        }

        public bool ContainProductInStock(int productId) 
            => products.ContainsKey(productId);

        public Product SearchProductByName(string productName)
        => products.Values.ToList().Where(p => p.name == productName).FirstOrDefault();//the default is null
        public Product SearchProductByProductId(int productId)
        => products.ContainsKey(productId) ? products[productId] : null;

        // ------------------------------ Purchase Policy ------------------------------
        public void AddPurchasePolicy(int memberId, int productId, PurchaseOption purchaseOption)
        {
            // todo: implement
        }
        public IList<Purchase> GetPurchaseHistory(int memberId)
        {
            // todo: implement, CHECK THAT HAVE PROPER PERMISSION
            return null;
        }

        // ------------------------------ Permission and Roles ------------------------------
        
        // cc 3
        // r 4.4
        public void MakeCoOwner(int requestingMemberId, int newCoOwnerMemberId) {
            string permissionError = CheckAtLeastCoOwnerPermission(requestingMemberId);
            if (permissionError != null)
                throw new MarketException("Could not make owner: " + permissionError);

            if (IsCoOwner(newCoOwnerMemberId))
                throw new MarketException(StoreErrorMessage("The member is already a CoOwner")); 
            if (IsManager(newCoOwnerMemberId))
                throw new MarketException(StoreErrorMessage("The member is already a Manager"));
            if (!IsMember(newCoOwnerMemberId))
                throw new MarketException("The requested new CoOwner is not a member");

            rolesInStore[Role.Owner].Add(newCoOwnerMemberId);

            appointmentsHierarchy.AddToHierarchy(membersGetter(requestingMemberId), membersGetter(newCoOwnerMemberId));
        }

        // cc 3
        // r 4.6, r 5
        public void MakeManager(int requestingMemberId, int newCoManagerMemberId)
        {
            string permissionError = CheckAtLeastManagerWithPermission(requestingMemberId, Permission.MakeCoManager);
            if (permissionError != null)
                throw new MarketException("Could not make manager: " + permissionError);

            if (IsCoOwner(newCoManagerMemberId))
                throw new MarketException(StoreErrorMessage("The member is already a CoOwner"));
            if (IsManager(newCoManagerMemberId))
                throw new MarketException(StoreErrorMessage("The member is already a Manager"));
            if (!IsMember(newCoManagerMemberId))
                throw new MarketException("The requested new CoOwner is not a member");

            rolesInStore[Role.Manager].Add(newCoManagerMemberId);

            IList<Permission> managerPermissions = new List<Permission>();
            managerPermissions.Add(Permission.RecieveInfo);
            managersPermissions[newCoManagerMemberId] = managerPermissions;

            appointmentsHierarchy.AddToHierarchy(membersGetter(requestingMemberId), membersGetter(newCoManagerMemberId));
            // todo: add tests checking this field has been changed
        }

        // r 4.7, r 5
        public void ChangeManagerPermissions(int requestingMemberId, int managerMemberId, IList<Permission> newPermissions) {
            // we allow this only to coOwners
            string permissionError = CheckAtLeastCoOwnerPermission(requestingMemberId);
            if (permissionError != null)
                throw new MarketException("Could not changed manager permissions: " + permissionError);

            if (!IsManager(managerMemberId))
                throw new ArgumentException(StoreErrorMessage("The id: " + managerMemberId + " is not of a managaer"));

            managersPermissions[managerMemberId] = newPermissions;
        }

        // r 4.11, r 5
        public IDictionary<Role, IList<int>> GetMembersRoles(int memberId) {
            // todo: implement. should be a part of the GetMembersInfo Transaction
            return null;
        }

        // r 4.11, r 5
        public IDictionary<int, IList<Permission>> GetManagersPermissions(int memberId)
        {
            // todo: implement. should be a part of the GetMembersInfo Transaction
            // todo: maybe improve the implementation
            return new Dictionary<int, IList<Permission>>(managersPermissions);
        }

        public bool IsFounder(int memberId)
        {
            return founder.GetId() == memberId;
        }

        public bool IsCoOwner(int memberId)
        {
            return rolesInStore[Role.Owner].Contains(memberId);
        }

        public bool IsManager(int memberId)
        {
            return rolesInStore[Role.Manager].Contains(memberId);
        }

        public bool HasPermission(int managerId, Permission permission)
        {
            if (!IsManager(managerId))
                throw new ArgumentException(StoreErrorMessage("The id: " + managerId + " is not of a managaer")); 
            return managersPermissions[managerId].Contains(permission);
            // todo: check if we want to have Member as key and not memberId,
            // it won't surly work and we can also get the member by its id
        }

        public bool IsManagerWithPermission(int memberId, Permission permission)
        {
            return IsManager(memberId) && HasPermission(memberId, permission);
        }

        // ------------------------------ General ------------------------------

        // 4.9
        public void CloseStore(int memberId)
        {
            // todo: implement
        }

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

    }
}