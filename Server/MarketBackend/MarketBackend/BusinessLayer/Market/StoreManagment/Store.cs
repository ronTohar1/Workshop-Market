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
            // todo: implement
        }

        // cc 3
        // r 4.6, r 5
        public void MakeCoManager(int requestingMemberId, int newCoManagerMemberId)
        {
            // todo: implement
        }

        // r 4.7, r 5
        public void ChangeManagerPermission(int requestingMemberId, int managerMemberId, IList<Permission> newPermissions) {
            // todo: implement
        }

        // r 4.11, r 5
        public IDictionary<Role, IList<int>> GetMembersRoles(int memberId) {
            // todo: implement. should be a part of the GetMembersInfo Transaction
            return null;
        }

        // r 4.11, r 5
        public IDictionary<int, IList<Permission>> GetManagersPermission(int memberId)
        {
            // todo: implement. should be a part of the GetMembersInfo Transaction
            return null;
        }

        // ------------------------------ General ------------------------------

        // 4.9
        public void CloseStore(int memberId)
        {
            // todo: implement
        }

    }
}