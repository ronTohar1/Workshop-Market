using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions
{
    public class ConditionDiscount : IConditionalExpression
    {
        private IPredicateExpression pred;
        private IDiscountExpression then;

        public ConditionDiscount(IPredicateExpression pred, IDiscountExpression then)
        {
            this.pred = pred;
            this.then = then;
        }

        public static ConditionDiscount DataConditionDiscountToConditionDiscount(DataConditionDiscount dataConditionDiscount)
        {
            return new ConditionDiscount(IPredicateExpression.DataPredicateExpressionToIPredicateExpression(dataConditionDiscount.Predicate), 
                IDiscountExpression.DataDiscountExpressionToIDiscountExpression(dataConditionDiscount.DiscountExpression)); 
        }

        // if [] then []
        public double EvaluateDiscount(ShoppingBag bag, Store store)
        {
            if (pred.EvaluatePredicate(bag, store))
                return then.EvaluateDiscount(bag, store);
            return 0;
        }

    }
}
