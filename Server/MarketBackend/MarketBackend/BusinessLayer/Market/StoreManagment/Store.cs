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


	    public Store(string storeName, Member founder)
	    {
            this.name = storeName;
            this.founder = founder;
            this.storeOwnersHirerachy = new Hierarchy<Member>(founder);
            this.purchaseHistory = new SynchronizedCollection<Purchase>();
            this.policy = new StorePolicy();
            this.products = new ConcurrentDictionary<int,Product>();
            this.managersPermissions = new ConcurrentDictionary<Member, IList<Permission>>();
            this.rolesInStore = new ConcurrentDictionary<Role, IList<Member>>();
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
        public void MakeCoOwner(int memberId, int targetMemberId) {
            // todo: implement
        }
        public void MakeCoManager(int memberId, int targetMemberId)
        {
            // todo: implement
        }
        public void ChangeManagerPermission(int memberId, int targetMemberId, IList<Permission> permissions) {
            // todo: implement
        }
        public IDictionary<Role, IList<int>> GetMembersRoles(int memberId) {
            // todo: implement. should be a part of the GetMembersInfo Transaction
            return null;
        }
        public IDictionary<int, IList<Permission>> GetManagersPermission(int memberId)
        {
            // todo: implement. should be a part of the GetMembersInfo Transaction
            return null;
        }

        // ------------------------------ General ------------------------------

        public void CloseStore(int memberId)
        {
            // todo: implement
        }

    }
}