using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts
{
    internal abstract class BagValuePredicate : IDiscountExpression
    {
        private int worth;
        
        public BagValuePredicate(int worth)
        {
            this.worth = worth;
        }

        public int EvaluateDiscount(ShoppingBag bag)
        {
            throw new NotImplementedException();
        }

        public bool EvaluatePredicate(ShoppingBag bag)
        {
            throw new NotImplementedException();
        }
    }
}
