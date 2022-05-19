using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies
{
    public class AtMostAmountRestriction : AtlestAmountRestriction
    {

        public AtMostAmountRestriction(int productId, int amount) : base(productId, amount) { }

        public override bool IsSatisfied(ShoppingBag bag)
        {
            return !base.IsSatisfied(bag);      
        }
    }
}
