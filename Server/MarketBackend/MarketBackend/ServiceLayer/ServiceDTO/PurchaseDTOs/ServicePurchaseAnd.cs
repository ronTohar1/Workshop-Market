using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    public class ServicePurchaseAnd : ServicePurchasePolicy
    {
        public ServiceRestriction firstPred { get; set; }
        public ServiceRestriction secondPred { get; set; }

        public ServicePurchaseAnd(ServiceRestriction firstPred, ServiceRestriction secondPred, string tag = "") : base(tag)
        {
            this.firstPred = firstPred;
            this.secondPred = secondPred;
        }
    }
}
