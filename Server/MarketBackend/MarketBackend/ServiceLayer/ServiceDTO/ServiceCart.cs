using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    internal class ServiceCart
    {
        internal IDictionary<int, ServiceShoppingBag> ShoppingBags { get; }

        public ServiceCart() =>
            ShoppingBags = new Dictionary<int, ServiceShoppingBag>();

        public bool IsEmpty() =>
            ShoppingBags.Count == 0;
    }
}
