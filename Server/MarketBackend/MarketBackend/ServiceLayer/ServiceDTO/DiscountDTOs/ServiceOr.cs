using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceOr : ServiceLogical
    {
        public ServiceOr(ServicePredicate firstExpression, ServicePredicate secondExpression, string tag = "")  : base(firstExpression, secondExpression, tag)
        {

        }
    }
}
