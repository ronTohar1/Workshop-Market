using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;


namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators
{
    internal class OrExpression : LogicalExpression
    {
        public OrExpression(IPredicateExpression firstExpression, IPredicateExpression secondExpression) : base(firstExpression, secondExpression)
        {

        }

        public override bool EvaluatePredicate(ShoppingBag bag)
        {
            return firstExpression.EvaluatePredicate(bag) || secondExpression.EvaluatePredicate(bag);
        }
    }
}
