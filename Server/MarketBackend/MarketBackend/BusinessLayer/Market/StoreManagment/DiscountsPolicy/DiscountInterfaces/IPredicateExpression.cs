
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.PredicatesExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces
{
 
    public interface IPredicateExpression
    {
        public bool EvaluatePredicate(ShoppingBag bag, Store store);
        public DataPredicateExpression IPredicateExpressionToDataPredicateExpression();
        public void RemoveFromDB();
        public static IPredicateExpression DataPredicateExpressionToIPredicateExpression(DataPredicateExpression dataPredicateExpression)
        {
            // LogicalExpression or BagValuePredicate or ProductAmountPredicate

            if (dataPredicateExpression is DataLogicalExpression)
                return LogicalExpression.DataLogicalExpressionToLogicalExpression((DataLogicalExpression)dataPredicateExpression);
            else if (dataPredicateExpression is DataBagValuePredicate)
                return BagValuePredicate.DataBagValuePredicateToBagValuePredicate((DataBagValuePredicate)dataPredicateExpression);
            else if (dataPredicateExpression is DataProductAmountPredicate)
                return ProductAmountPredicate.DataProductAmountPredicateToProductAmountPredicate((DataProductAmountPredicate)dataPredicateExpression); 
            else
                throw new Exception("not supporting this inherent of IPredicateExpression");
        }
    }
}
