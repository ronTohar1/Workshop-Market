using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts
{
    internal class BagValuePredicate : IPredicateExpression
    {
        private int worth;
        
        public BagValuePredicate(int worth)
        {
            this.worth = worth;
        }

        //check of the bag worth more than worth
        public bool EvaluatePredicate(ShoppingBag bag)
        {
            throw new NotImplementedException();
        }
    }
}
