using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServicePurchaseAttempt
    {
        public bool purchaseSucceeded { get; private set; }
        public string failedMessage { get; private set; }
        public ServicePurchase purchaseContent { get; private set; }
    }
}
