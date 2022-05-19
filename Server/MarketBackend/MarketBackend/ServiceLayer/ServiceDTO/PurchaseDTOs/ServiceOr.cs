using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    internal class ServiceOr : IServicePurchase
    {
        public IServiceRestriction firstPred { get; set; }
        public IServiceRestriction secondPred { get; set; }

        public ServiceOr(IServiceRestriction firstPred, IServiceRestriction secondPred)
        {
            this.firstPred = firstPred;
            this.secondPred = secondPred;
        }
    }
}
