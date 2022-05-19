using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies
{
    public class BeforeHourProductRestriction : BeforeHourRestriction
    {
        public int productId { get; set; }
        public int amount { get; set; }

        public BeforeHourProductRestriction(int hour, int productId, int amount) : base(hour)
        {
            this.productId = productId;
            this.amount = amount;
        }

        public override bool IsSatisfied(ShoppingBag bag)
        {
            //true if after hours, false if before
            bool cond = base.IsSatisfied(bag);

            //if before we need to check we dont have the more than amount of the product
            if (!cond)
            {
                foreach(ProductInBag pib in bag.ProductsAmounts.Keys)
                {
                    if (pib.ProductId == productId)
                    {
                        return bag.ProductsAmounts[pib] < amount;
                    }
                }
            }
            return true;
        }

    }
}
