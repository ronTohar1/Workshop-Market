using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    internal class ServiceBeforeHour : ServiceAfterHour
    {
        public ServiceBeforeHour(int hour) : base(hour) { }
    }
}
