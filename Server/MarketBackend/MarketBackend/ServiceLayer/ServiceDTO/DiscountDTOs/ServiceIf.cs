using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceIf : ServiceExpression
    {
        public ServicePredicate test { get; set; }
        public ServiceDiscount thenDis { get; set; }
        public ServiceDiscount elseDis { get; set; }

        public ServiceIf(ServicePredicate test, ServiceDiscount thenDis, ServiceDiscount elseDis, string tag = "") : base(tag)
        {
            this.test = test;
            this.thenDis = thenDis;
            this.elseDis = elseDis;
        }
    }
}
