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
        public IList<Purchase> purchaseHistory { get; }
        public StorePolicy policy { get; }
        public IDictionary<int,Product> products { get; }
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

        // --------------- Products actions: --------------
        public void AddNewProduct(int memberId, string productName, double pricePerUnit) {
            // todo: implement
        }
        public void AddProductToIncentory(int memberId,string productName ,int amount) {
            // todo: implement
        }
        public void RemoveProduct() {
        }
        public virtual string GetName()
        {
            return name; 
        }

        public void CloseStore(int memberId)
        {
            // todo: implement
        }

    }
}