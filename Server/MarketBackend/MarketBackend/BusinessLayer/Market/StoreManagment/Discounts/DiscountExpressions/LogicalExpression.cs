using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions
{
    internal abstract class LogicalExpression : IPredicateExpression
    {
        public IPredicateExpression firstExpression { get; set; }
        public IPredicateExpression secondExpression { get; set; }

        public LogicalExpression(IPredicateExpression firstExpression, IPredicateExpression secondExpression)
        {
            this.firstExpression = firstExpression;
            this.secondExpression = secondExpression;
        }

        //should be overrided
        public virtual bool EvaluatePredicate(ShoppingBag bag, Store store)
        {
            throw new NotSupportedException();
        }

    }
}
