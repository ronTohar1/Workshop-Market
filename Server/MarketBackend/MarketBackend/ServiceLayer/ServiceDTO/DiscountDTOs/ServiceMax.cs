using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceMax : IServiceDiscount
    {
        public IList<IServiceDiscount> discounts { get; set; }
        public ServiceMax()
        {
            discounts = new List<IServiceDiscount>();
        }

        public void AddDiscount(IServiceDiscount discount)
        {
            discounts.Add(discount);
        }
    }
}
