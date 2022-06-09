using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    public class ServiceXor : ServiceLogical
    {
        public ServiceXor(ServicePredicate firstExpression, ServicePredicate secondExpression, string tag = "") : base(firstExpression, secondExpression, tag)
        {

        }
    }
}
