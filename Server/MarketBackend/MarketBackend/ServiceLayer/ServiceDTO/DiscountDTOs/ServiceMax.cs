using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    public class ServiceMax : ServiceDiscount
    {
        public IList<ServiceDiscount> discounts { get; set; }
        public ServiceMax(string tag = "") : base(tag)
        {
            discounts = new List<ServiceDiscount>();
        }

        public void AddDiscount(ServiceDiscount discount)
        {
            discounts.Add(discount);
        }
    }
}
