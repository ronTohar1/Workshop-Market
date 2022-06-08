using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceXorDiscount : ServiceLogicalDiscount
    {
        public ServiceXorDiscount(ServiceDiscount firstExpression, ServiceDiscount secondExpression, string tag = "") : base(firstExpression, secondExpression, tag)
        {

        }
    }
}
