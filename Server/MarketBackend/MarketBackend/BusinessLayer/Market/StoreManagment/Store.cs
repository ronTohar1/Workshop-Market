using MarketBackend.BusinessLayer.Buyers.Members;
using System;
using System.Collections.Concurrent;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class Store
    {
        public string name { get; }
        public Member founder { get; }
        public Hierarchy<Member> storeOwnersHirerachy { get; }
        public StorePolicy policy { get; }
        public IDictionary<int,Product> products { get; }
        
        private IList<Purchase> purchaseHistory;
        private IDictionary<Member, IList<Permission>> managersPermissions;
        private IDictionary<Role, IList<Member>> rolesInStore;
        private Func<int, Member> membersGetter; 


        // cc 5
        // cc 6
	    public Store(string storeName, Member founder, Func<int, Member> membersGetter)
	    {
            this.name = storeName;
            this.founder = founder;
            this.storeOwnersHirerachy = new Hierarchy<Member>(founder);
            this.purchaseHistory = new SynchronizedCollection<Purchase>();
            this.policy = new StorePolicy();
            this.products = new ConcurrentDictionary<int,Product>();
            this.managersPermissions = new ConcurrentDictionary<Member, IList<Permission>>();
            this.rolesInStore = new ConcurrentDictionary<Role, IList<Member>>();
            this.membersGetter = membersGetter;
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

        // todo: implement AddProductReview(memberId: int, review: String): bool

        // todo: implement GetProductReviews(productId: int): ... 

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
            string permissionError = HasPermission(requestingMemberId, true, null, true); 

        }

        // cc 3
        // r 4.6, r 5
        public void MakeManager(int requestingMemberId, int newCoManagerMemberId)
        {
            // todo: implement
        }

        // r 4.7, r 5
        public void ChangeManagerPermissions(int requestingMemberId, int managerMemberId, IList<Permission> newPermissions) {
            // todo: implement
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
            return null;
        }

        public bool IsFounder(int memberId)
        {
            return false;
        }

        public bool IsCoOwner(int memberId)
        {
            return false;
        }

        public bool IsManager(int memberId)
        {
            return false;
        }

        public bool HasPermission(int managerId, Permission permission)
        {
            if (!IsManager(managerId))
                throw new ArgumentException(StoreErrorMessage("The id: " + managerId + " is not of a managaer")); 
            return managersPermissions[managerId].Contains(permission);
        }

        // ------------------------------ General ------------------------------

        // 4.9
        public void CloseStore(int memberId)
        {
            // todo: implement
        }

        // returns null if there is permission or a string describing why not otherwise
        // the permissions are in the following order:
        // founder --> coOwner --> manager --> member
        private string HasPermission(int memberId, bool coOwnerHas, Permission[] permissionsEnough, bool memberHas)
        {
            // founder has permission for action
            if (IsFounder(memberId))
                return null;

            if (!coOwnerHas)
                return StoreErrorMessage("The member (of id: " + memberId + ") does not have the permission required in this store: Founder");
            
            // coOwner has permission for action
            if (IsCoOwner(memberId))
                return null; 

            if (permissionsEnough == null || permissionsEnough.Length == 0)
                return StoreErrorMessage("The member (of id: " + memberId + ") does not have the permission required in this store: CoOnwer");

            // some manager permissions are enough for action
            if (IsManager(memberId) && permissionsEnough.Count(permission => HasPermission(memberId, permission)) > 0)
                return null;

            if (!memberHas)
            {
                String errorMessage = "The member(of id: " + memberId + ") does not have the permission for: ";
                bool firstTime = true;
                foreach (Permission permission in permissionsEnough)
                {
                    if (!firstTime)
                        errorMessage += "| ";
                    else
                        firstTime = false;
                    errorMessage += permission.ToString() + " ";
                }
                return StoreErrorMessage(errorMessage);
            }

            // members have permission for this action
            if (IsMember(memberId))
                return null;

            return StoreErrorMessage("The id: " + memberId + " is not of a member");
        }

        private bool IsMember(int memberId) // should be private
        {
            return false;
        }

        private string StoreErrorMessage(string errorMessage)
        {
            return errorMessage + " in the store: " + name; 
        }

    }
}