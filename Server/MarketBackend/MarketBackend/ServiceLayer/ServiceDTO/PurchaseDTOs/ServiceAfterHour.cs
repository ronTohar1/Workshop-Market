using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    internal class ServiceAfterHour : IServiceRestriction
    {
        public int hour { get; set; }

        public ServiceAfterHour(int hour)
        {
            this.hour = hour;
        }
    }
}
