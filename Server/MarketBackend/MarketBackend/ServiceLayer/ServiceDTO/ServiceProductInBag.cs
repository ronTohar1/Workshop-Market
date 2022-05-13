using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    internal class ServiceProductInBag
    {
        public int ProductId { get; }
        public int StoreId { get; }

        public ServiceProductInBag(ProductInBag pib)
        {
            ProductId = pib.ProductId;
            StoreId = pib.StoreId;
        }

        public override bool Equals(object? obj)
        {

            if (obj == null || GetType() != obj.GetType())
                return false;

            ServiceProductInBag other = (ServiceProductInBag)obj;
            return ProductId == other.ProductId && StoreId == other.StoreId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductId, StoreId);
        }
    }
}
