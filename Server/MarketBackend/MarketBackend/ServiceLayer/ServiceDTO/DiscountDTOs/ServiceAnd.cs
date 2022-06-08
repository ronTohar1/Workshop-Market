using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceAnd : ServiceLogical
    {
        public ServiceAnd(ServicePredicate firstExpression, ServicePredicate secondExpression, string tag = "") : base(firstExpression, secondExpression, tag)
        {

        }
    }
}
