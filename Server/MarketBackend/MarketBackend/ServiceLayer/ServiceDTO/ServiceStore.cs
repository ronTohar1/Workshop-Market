using MarketBackend.BusinessLayer.Market.StoreManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServiceStore
    {
        public int Id { get; }
        public string Name { get; }

        public bool IsOpen { get; }
        public ServiceMember Founder { get; }
        public IList<ServiceProduct> Products { get; private set; }

        public IList<ServicePurchasePolicy> PurchasePolicies { get; set; }
        public IList<ServiceDiscountPolicy> DiscountPolicies { get; set; }

        public IList<ServiceBid> Bids { get; }

        public ServiceStore(int id, Store store, IList<ServiceProduct> productsIds)
        {
            Id = id;
            Name = store.name;
            Products = productsIds;
            IsOpen = store.isOpen;
            Founder = new ServiceMember(store.founder);
            DiscountPolicies = store.discountManager.discounts.Values.Select(disc => new ServiceDiscountPolicy(disc.id, disc.description)).ToList();
            PurchasePolicies = store.purchaseManager.purchases.Values.Select(purch => new ServicePurchasePolicy(purch.id, purch.description)).ToList();
            Bids = store.bids.Values.Select(bid => new ServiceBid(bid.id, bid.storeId, bid.productId, bid.memberId, bid.bid, bid.aprovingIds, bid.counterOffer,bid.offer)).ToList();
           
        }

        public override bool Equals(Object? other)
        {
            if (other == null || !(other is ServiceStore))
                return false;
            ServiceStore otherStore = (ServiceStore)other;
            if (otherStore.Products == null)
                return false;
            return this.Id == otherStore.Id && this.Name.Equals(otherStore.Name) && SameElements(this.Products, otherStore.Products);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Products.GetHashCode()); // todo: make sure this is okay
        }

        private bool SameElements<T>(IList<T> list1, IList<T> list2)
        {
            if (list1.Count != list2.Count)
                return false;
            return list1.All(element => list2.Contains(element));

        }
    }
}
