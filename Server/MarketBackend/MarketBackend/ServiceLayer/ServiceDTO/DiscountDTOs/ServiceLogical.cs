using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    public class ServiceLogical : ServicePredicate
    {
        public ServicePredicate firstExpression { get; set; }
        public ServicePredicate secondExpression { get; set; }

        public ServiceLogical(ServicePredicate firstExpression, ServicePredicate secondExpression, string tag = "") : base(tag)
        {
            this.firstExpression = firstExpression;
            this.secondExpression = secondExpression;
        }
    }
}
