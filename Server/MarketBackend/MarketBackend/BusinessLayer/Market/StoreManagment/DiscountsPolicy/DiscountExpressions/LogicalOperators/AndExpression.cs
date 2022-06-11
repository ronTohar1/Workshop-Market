using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators
{
    public class AndExpression : LogicalExpression
    {
        public AndExpression(IPredicateExpression firstExpression, IPredicateExpression secondExpression) : base(firstExpression, secondExpression)
        {

        }

        public static AndExpression DataAndExpressionToAndExpression(DataAndExpression dataAndExpression)
        {
            return new AndExpression(
                IPredicateExpression.DataPredicateExpressionToIPredicateExpression(dataAndExpression.First),
                IPredicateExpression.DataPredicateExpressionToIPredicateExpression(dataAndExpression.Second)
                );
        }

        //if [] and [] then yes
        public override bool EvaluatePredicate(ShoppingBag bag, Store store)
        {
            return firstExpression.EvaluatePredicate(bag, store) && secondExpression.EvaluatePredicate(bag, store);
        }
    }
}
