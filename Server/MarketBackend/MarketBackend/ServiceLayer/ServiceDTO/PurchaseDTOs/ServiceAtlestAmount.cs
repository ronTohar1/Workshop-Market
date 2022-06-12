using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    public class ServiceAtlestAmount : ServiceRestriction
    {
        public int productId { get; set; }
        public int amount { get; set; }

        public ServiceAtlestAmount(int productId, int amount, string tag = "") : base(tag)
        {
            this.productId = productId;
            this.amount = amount;
        }
    }
}
