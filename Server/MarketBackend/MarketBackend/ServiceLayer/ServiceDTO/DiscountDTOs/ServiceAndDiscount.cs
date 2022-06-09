using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    public class ServiceAndDiscount : ServiceLogicalDiscount
    {
        public ServiceAndDiscount(ServiceDiscount firstExpression, ServiceDiscount secondExpression,string tag = "") : base(firstExpression, secondExpression, tag)
        {

        }
    }
}
