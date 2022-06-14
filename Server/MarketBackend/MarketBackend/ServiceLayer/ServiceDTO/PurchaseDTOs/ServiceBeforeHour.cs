using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    public class ServiceBeforeHour : ServiceAfterHour
    {
        public ServiceBeforeHour(int hour, string tag = ""): base(hour, tag) { }
    }
}
