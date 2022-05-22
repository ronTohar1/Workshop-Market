using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    internal class ServiceAtlestAmount : IServiceRestriction
    {
        public int productId { get; set; }
        public int amount { get; set; }

        public ServiceAtlestAmount(int productId, int amount)
        {
            this.productId = productId;
            this.amount = amount;
        }
    }
}
