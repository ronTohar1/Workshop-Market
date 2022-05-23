using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceIf : IServiceExpression
    {
        public IServicePredicate test { get; set; }
        public IServiceDiscount thenDis { get; set; }
        public IServiceDiscount elseDis { get; set; }

        public ServiceIf(IServicePredicate test, IServiceDiscount thenDis, IServiceDiscount elseDis)
        {
            this.test = test;
            this.thenDis = thenDis;
            this.elseDis = elseDis;
        }
    }
}
