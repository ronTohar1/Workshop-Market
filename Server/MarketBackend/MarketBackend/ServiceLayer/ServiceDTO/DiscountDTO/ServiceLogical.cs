using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceLogical : IServicePredicate
    {
        public IServicePredicate firstExpression { get; set; }
        public IServicePredicate secondExpression { get; set; }

        public ServiceLogical(IServicePredicate firstExpression, IServicePredicate secondExpression)
        {
            this.firstExpression = firstExpression;
            this.secondExpression = secondExpression;
        }
    }
}
