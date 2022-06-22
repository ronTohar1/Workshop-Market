using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.RestrictionPolicies;
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

        public static AtMostAmountRestriction DataAtMostAmountRestrictionToAtMostAmountRestriction(DataAtMostAmountRestriction dataAtMostAmountRestriction)
        {
            return new AtMostAmountRestriction(dataAtMostAmountRestriction.ProductId, dataAtMostAmountRestriction.Amount); 
        }

        public override bool IsSatisfied(ShoppingBag bag)
        {
            foreach (ProductInBag pib in bag.ProductsAmounts.Keys)
            {
                if (pib.ProductId == productId)
                {
                    return bag.ProductsAmounts[pib] > amount;
                }
            }
            return true;
        }

        public override DataIPurchasePolicy IPurchasePolicyToDataIPurchasePolicy()
        {
            return new DataAtMostAmountRestriction()
            {
                Amount = amount,
                ProductId = productId
            };
        }

        public override void RemoveFromDB(DataIPurchasePolicy dpp)
        {
            DataAtMostAmountRestriction daar = (DataAtMostAmountRestriction)dpp;
            //TODO myself
        }
    }
}
