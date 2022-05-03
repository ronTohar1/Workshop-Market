using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts
{
    internal abstract class PredicateExpression
    {
        private IDiscountExpression discount;

        public PredicateExpression(IDiscountExpression discount)
        {
            this.discount = discount;
        }

        public bool EvaluatePredicate(ShoppingBag bag)
        {
            throw new NotImplementedException();
        }
    }
}
