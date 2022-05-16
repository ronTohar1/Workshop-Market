using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceBagValue : IServicePredicate
    {
        public int worth { get; set; }

        public ServiceBagValue(int worth)
        {
            this.worth = worth;
        }
    }
}
