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
            foreach (ProductInBag pib in bag.ProductsAmounts.Keys)
            {
                if (pib.ProductId == productId)
                {
                    return bag.ProductsAmounts[pib] <= amount;
                }
            }
            return true;
        }
    }
}
