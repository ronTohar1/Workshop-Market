using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    public class ServicePurchasePredicate
    {
        public string tag { get; set; }
        public ServicePurchasePredicate(string tag = "")
        {
            this.tag = tag;
        }
    }
}
