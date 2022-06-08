using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceStoreDiscount : ServiceDiscount
    {
        public int discount { get; set; }

        public ServiceStoreDiscount(int discount, string tag = "") : base(tag)
        {
            this.discount = discount;
        }
    }
}
