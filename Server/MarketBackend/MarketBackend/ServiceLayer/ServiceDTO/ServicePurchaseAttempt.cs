using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Market.StoreManagment;
namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServicePurchaseAttempt
    {
        public bool purchaseSucceeded { get; private set; }
        public string failedMessage { get; private set; }
        public ServicePurchase purchaseContent { get; private set; }

        public ServicePurchaseAttempt(PurchaseAttempt purchaseAttempt) {
            purchaseSucceeded = purchaseAttempt.purchaseSucceeded;
            failedMessage = purchaseAttempt.failedMessage;
            Purchase businessPurchase = purchaseAttempt.purchaseContent;
            purchaseContent = new ServicePurchase(businessPurchase.purchaseDate, businessPurchase.purchasePrice, businessPurchase.purchaseDescription);
        }
    }
}
