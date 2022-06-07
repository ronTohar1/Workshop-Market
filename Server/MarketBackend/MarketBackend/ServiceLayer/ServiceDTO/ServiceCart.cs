using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServiceCart
    {
        public IDictionary<int, ServiceShoppingBag> ShoppingBags { get; }

        public ServiceCart() =>
            ShoppingBags = new Dictionary<int, ServiceShoppingBag>();

        public ServiceCart(Cart c)
        {
            ShoppingBags = new Dictionary<int, ServiceShoppingBag>();

            IDictionary<int, ShoppingBag> sb = c.ShoppingBags;
            foreach (int id in sb.Keys)
                ShoppingBags.Add(id, new ServiceShoppingBag(sb[id]));
        }

        public bool IsEmpty() =>
            ShoppingBags.Count == 0;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ServiceCart otherServiceCart = (ServiceCart)obj;
            return IsEqualShoppingBags(otherServiceCart);

        }

        private bool IsEqualShoppingBags(ServiceCart otherServiceCart)
        {
            if (ShoppingBags.Count == otherServiceCart.ShoppingBags.Count)
            {
                foreach (KeyValuePair<int, ServiceShoppingBag> storeBag in otherServiceCart.ShoppingBags)
                {
                    int storeId = storeBag.Key;
                    ServiceShoppingBag bag = storeBag.Value;

                    if (!this.ShoppingBags.ContainsKey(storeId))
                        return false;

                    if (!this.ShoppingBags[storeId].Equals(bag))
                        return false;

                }
                return true;
            }
            return false;
        }
    }
}
