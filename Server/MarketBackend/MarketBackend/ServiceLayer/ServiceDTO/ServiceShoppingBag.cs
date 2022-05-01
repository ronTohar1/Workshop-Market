using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    internal class ServiceShoppingBag
    {
        private IDictionary<ServiceProductInBag, int> productsAmounts;
        internal IDictionary<ServiceProductInBag, int> ProductsAmounts { get { return productsAmounts; } }
        public ServiceShoppingBag(ShoppingBag sb)
        {
            IDictionary<ProductInBag, int> pam = sb.ProductsAmounts;

            productsAmounts = new Dictionary<ServiceProductInBag, int>();
            foreach (ProductInBag pib in pam.Keys)
                productsAmounts.Add(new ServiceProductInBag(pib), pam[pib]);
        }
    }
}
