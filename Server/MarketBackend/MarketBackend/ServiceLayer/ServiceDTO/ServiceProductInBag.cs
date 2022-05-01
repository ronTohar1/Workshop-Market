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
    }
}
