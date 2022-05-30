﻿using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServiceShoppingBag
    {
        public IDictionary<ServiceProductInBag, int> ProductsAmounts { get; private set; }
        public int StoreId { get; private set; }
        public ServiceShoppingBag(ShoppingBag sb)
        {
            this.StoreId = sb.StoreId;
            IDictionary<ProductInBag, int> pam = sb.ProductsAmounts;

            ProductsAmounts = new Dictionary<ServiceProductInBag, int>();
            foreach (ProductInBag pib in pam.Keys)
                ProductsAmounts.Add(new ServiceProductInBag(pib), pam[pib]);
     
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

            foreach (KeyValuePair<ServiceProductInBag, int> item in other.ProductsAmounts)
            {
                ServiceProductInBag productInBag = item.Key;
                int amount = item.Value;

                if (!this.ProductsAmounts.ContainsKey(productInBag))
                    return false;
                if (!this.ProductsAmounts[productInBag].Equals(amount))
                    return false;
            }

            return true;
        }
    }

    
}
