using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators
{
    public class OrExpression : LogicalExpression
    {
        public OrExpression(IPredicateExpression firstExpression, IPredicateExpression secondExpression) : base(firstExpression, secondExpression)
        {

        }

        public static OrExpression DataOrExpressionToOrExpression(DataOrExpression dataOrExpression)
        {
            return new OrExpression(
                IPredicateExpression.DataPredicateExpressionToIPredicateExpression(dataOrExpression.First),
                IPredicateExpression.DataPredicateExpressionToIPredicateExpression(dataOrExpression.Second)
                );
        }

        //if [] or [] then yes
        public override bool EvaluatePredicate(ShoppingBag bag, Store store)
        {
            return firstExpression.EvaluatePredicate(bag, store) || secondExpression.EvaluatePredicate(bag, store);
        }
    }
}
