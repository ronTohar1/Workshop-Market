using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceProductDiscount : ServiceStoreDiscount
    {
        public int productId { get; set; }

        public ServiceProductDiscount(int productId, int discount,string tag = "") : base(discount, tag)
        {
            this.productId = productId;
        }
    }
}
