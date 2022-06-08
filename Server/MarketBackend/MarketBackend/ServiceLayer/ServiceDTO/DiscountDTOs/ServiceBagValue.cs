using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceBagValue : ServicePredicate
    {
        public int worth { get; set; }

        public ServiceBagValue(int worth,string tag = "") : base(tag)
        {
            this.worth = worth;
        }
    }
}
