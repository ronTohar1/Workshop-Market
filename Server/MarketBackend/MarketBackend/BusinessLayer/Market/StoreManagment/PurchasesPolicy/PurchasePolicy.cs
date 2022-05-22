using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
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

        public bool CanBuy(ShoppingBag bag)
        {
            return policy.IsSatisfied(bag);
        }


    }
}
