using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceConditionDiscount : IServiceConditional
    {
        public IServicePredicate pred { get; set; }
        public IServiceDiscount then { get; set; }

        public ServiceConditionDiscount(IServicePredicate pred, IServiceDiscount then)
        {
            this.pred = pred;
            this.then = then;
        }
    }
}
