using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceConditionDiscount : ServiceConditional
    {
        public ServicePredicate pred { get; set; }
        public ServiceDiscount then { get; set; }

        public ServiceConditionDiscount(ServicePredicate pred, ServiceDiscount then, string tag = "") : base(tag)
        {
            this.pred = pred;
            this.then = then;
        }
    }
}
