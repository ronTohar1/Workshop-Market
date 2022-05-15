using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;


namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators
{
    internal class XorExpression : LogicalExpression
    {
        public XorExpression(IPredicateExpression firstExpression, IPredicateExpression secondExpression) : base(firstExpression, secondExpression)
        {

        }

        // if ([] and ![]) or (![] and []) 
        public override bool EvaluatePredicate(ShoppingBag bag, Store store)
        {
            return (firstExpression.EvaluatePredicate(bag, store) && !secondExpression.EvaluatePredicate(bag, store))
                || (secondExpression.EvaluatePredicate(bag, store) && !firstExpression.EvaluatePredicate(bag, store));
        }
    }
}
