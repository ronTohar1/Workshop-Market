using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    internal class ServiceBeforeHourProduct : ServiceBeforeHour
    {
        public int productId { get; set; }
        public int amount { get; set; }

        public ServiceBeforeHourProduct(int hour, int productId, int amount) : base(hour)
        {
            this.productId = productId;
            this.amount = amount;
        }
    }
}
