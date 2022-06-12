using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy
{
    public class PurchasePolicy
    {
        public IPurchasePolicy policy { get; set; }
        public string description { get; set; }
        public int id { get; set; }

        public PurchasePolicy(int id, string description, IPurchasePolicy policy)
        {
            this.id = id;
            this.description = description;
            this.policy = policy;
        }

        public static PurchasePolicy DataPurchasePolicyToPurchasePolicy(DataPurchasePolicy dataPurchasePolicy)
        {
            return new PurchasePolicy(dataPurchasePolicy.Id, dataPurchasePolicy.Description,
                IPurchasePolicy.DataIPurchasePolicyToIPurchasePolicy(dataPurchasePolicy.Policy));
        }

        public bool CanBuy(ShoppingBag bag)
        {
            return policy.IsSatisfied(bag);
        }
    }
}
