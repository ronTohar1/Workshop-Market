using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    public class ServiceAfterHourProduct : ServiceAfterHour
    {
        public int productId { get; set; }
        public int amount { get; set; }

        public ServiceAfterHourProduct(int hour, int productId, int amount, string tag = "") : base(hour, tag)
        {
            this.productId = productId;
            this.amount = amount;
        }
    }
}
