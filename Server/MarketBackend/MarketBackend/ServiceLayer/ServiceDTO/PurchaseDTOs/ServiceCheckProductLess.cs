using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    internal class ServiceCheckProductLess : IServicePredicate
    {
        public int productId { get; set; }
        public int amount { get; set; }

        public ServiceCheckProductLess(int productId, int amount)
        {
            this.productId = productId;
            this.amount = amount;
        }
    }
}
