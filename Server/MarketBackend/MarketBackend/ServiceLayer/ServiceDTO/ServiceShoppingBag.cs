using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServiceShoppingBag
    {
        private IDictionary<ServiceProductInBag, int> productsAmounts;
        public IDictionary<ServiceProductInBag, int> ProductsAmounts { get { return productsAmounts; } }
        public ServiceShoppingBag(ShoppingBag sb)
        {
            IDictionary<ProductInBag, int> pam = sb.ProductsAmounts;

            productsAmounts = new Dictionary<ServiceProductInBag, int>();
            foreach (ProductInBag pib in pam.Keys)
                productsAmounts.Add(new ServiceProductInBag(pib), pam[pib]);
     
        }

        public override bool Equals(object? obj)
        {

            if (obj == null || GetType() != obj.GetType())
                return false;

            ServiceShoppingBag other = (ServiceShoppingBag)obj;
            return IsEqualProductAmounts(other);

        }

        private bool IsEqualProductAmounts(ServiceShoppingBag other)
        {

            foreach (KeyValuePair<ServiceProductInBag, int> item in other.productsAmounts)
            {
                ServiceProductInBag productInBag = item.Key;
                int amount = item.Value;

                if (!this.productsAmounts.ContainsKey(productInBag))
                    return false;
                if (!this.productsAmounts[productInBag].Equals(amount))
                    return false;
            }

            return true;
        }
    }

    
}
