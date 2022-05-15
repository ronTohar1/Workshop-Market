using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;


namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators
{
    internal class OrExpression : LogicalExpression
    {
        public OrExpression(IPredicateExpression firstExpression, IPredicateExpression secondExpression) : base(firstExpression, secondExpression)
        {

        }

        //if [] or [] then yes
        public override bool EvaluatePredicate(ShoppingBag bag, Store store)
        {
            return firstExpression.EvaluatePredicate(bag, store) || secondExpression.EvaluatePredicate(bag, store);
        }
    }
}
