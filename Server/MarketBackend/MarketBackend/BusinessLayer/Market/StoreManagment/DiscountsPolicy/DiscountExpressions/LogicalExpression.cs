using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions
{
    public abstract class LogicalExpression : IPredicateExpression
    {
        public IPredicateExpression firstExpression { get; set; }
        public IPredicateExpression secondExpression { get; set; }

        public LogicalExpression(IPredicateExpression firstExpression, IPredicateExpression secondExpression)
        {
            this.firstExpression = firstExpression;
            this.secondExpression = secondExpression;
        }

        public static LogicalExpression DataLogicalExpressionToLogicalExpression(DataLogicalExpression dataLogicalExpression)
        {
            // And or Or or Xor 

            if (dataLogicalExpression is DataAndExpression)
                return AndExpression.DataAndExpressionToAndExpression((DataAndExpression)dataLogicalExpression);
            else if (dataLogicalExpression is DataOrExpression)
                return OrExpression.DataOrExpressionToOrExpression((DataOrExpression)dataLogicalExpression);
            else if (dataLogicalExpression is DataXorExpression)
                return XorExpression.DataXorExpressionToXorExpression((DataXorExpression)dataLogicalExpression);
            else
                throw new Exception("not supporting this inherent of LogicalExpression");
        }

        //should be overrided -- all of them
        public virtual bool EvaluatePredicate(ShoppingBag bag, Store store)
        {
            throw new NotSupportedException();
        }

        public virtual DataPredicateExpression IPredicateExpressionToDataPredicateExpression()
        {
            throw new NotSupportedException();
        }

        public virtual void RemoveFromDB(DataPredicateExpression dpe)
        {
            throw new NotSupportedException();
        }
    }
}
