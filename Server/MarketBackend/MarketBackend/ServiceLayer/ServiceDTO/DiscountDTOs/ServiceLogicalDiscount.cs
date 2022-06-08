using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceLogicalDiscount : ServiceDiscount
    {
        public ServiceDiscount firstExpression { get; set; }
        public ServiceDiscount secondExpression { get; set; }

        public ServiceLogicalDiscount(ServiceDiscount firstExpression, ServiceDiscount secondExpression, string tag = "") :base(tag)
        {
            this.firstExpression = firstExpression;
            this.secondExpression = secondExpression;
        }
    }
}
