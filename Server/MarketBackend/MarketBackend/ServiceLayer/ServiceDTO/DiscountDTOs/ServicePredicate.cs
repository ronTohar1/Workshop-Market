using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServicePredicate : ServiceExpression
    {
        public ServicePredicate(string tag = "") : base(tag) {
        
        }
    }
}
