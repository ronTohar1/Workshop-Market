using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class PurchaseAttempt
    {
        public bool purchaseSucceeded { get; private set; }
        public string failedMessage { get; private set; }
        public Purchase purchaseContent { get; private set; }
        public PurchaseAttempt(Purchase purchaseContent)// purchase suceeded constructor
        {
            this.purchaseSucceeded = true;
            this.purchaseContent = purchaseContent;
            this.failedMessage = string.Empty;
        }
        public PurchaseAttempt(string failedMessage)// purchase failed constructor
        {
            this.purchaseSucceeded = false;
            this.purchaseContent = null;
            this.failedMessage = failedMessage;
        }
    }
}
