using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.RestrictionPolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies
{
    public class AtlestAmountRestriction : IRestrictionExpression
    {
        public int productId { get; set; }
        public int amount { get; set; }

        public AtlestAmountRestriction(int productId, int amount)
        {
            this.productId = productId;
            this.amount = amount;
        }

        public static AtlestAmountRestriction DataAtlestAmountRestrictionToAtlestAmountRestriction(DataAtLeastAmountRestriction dataAtlestAmountRestriction)
        {
            // AtlestAmountRestriction or AtMostAmountRestriction

            if (dataAtlestAmountRestriction is DataAtMostAmountRestriction)
                return AtMostAmountRestriction.DataAtMostAmountRestrictionToAtMostAmountRestriction((DataAtMostAmountRestriction)dataAtlestAmountRestriction);
            return new AtlestAmountRestriction(dataAtlestAmountRestriction.ProductId, dataAtlestAmountRestriction.Amount); 
        }

        public virtual bool IsSatisfied(ShoppingBag bag)
        {
            foreach (ProductInBag pib in bag.ProductsAmounts.Keys)
            {
                if (pib.ProductId == productId)
                {
                    return bag.ProductsAmounts[pib] < amount;
                }
            }
            return false;
        }
    }
}
