using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions
{
    internal class ConditionDiscount : IDiscountExpression
    {
        private PredicateExpression pred;
        private IDiscountExpression then;

        public ConditionDiscount(PredicateExpression pred, IDiscountExpression then)
        {
            this.pred = pred;
            this.then = then;
        }

        public int EvaluateDiscount(ShoppingBag bag)
        {
            if (pred.EvaluatePredicate(bag))
            {
                return then.EvaluateDiscount(bag);
            }
            return 0;
        }


        //never
        public bool EvaluatePredicate(ShoppingBag bag)
        {
            throw new NotSupportedException();
        }
    }
}
