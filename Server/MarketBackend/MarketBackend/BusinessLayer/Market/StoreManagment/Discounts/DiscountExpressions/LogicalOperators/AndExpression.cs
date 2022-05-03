using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators
{
    internal class AndExpression : LogicalExpression
    {
        public AndExpression(IPredicateExpression firstExpression, IPredicateExpression secondExpression) : base(firstExpression, secondExpression)
        {

        }

        public override bool EvaluatePredicate(ShoppingBag bag)
        {
            return firstExpression.EvaluatePredicate(bag) && secondExpression.EvaluatePredicate(bag);
        }
    }
}
