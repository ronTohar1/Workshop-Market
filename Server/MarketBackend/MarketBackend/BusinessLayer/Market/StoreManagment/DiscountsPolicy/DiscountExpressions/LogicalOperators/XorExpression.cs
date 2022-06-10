﻿using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators
{
    public class XorExpression : LogicalExpression
    {
        public XorExpression(IPredicateExpression firstExpression, IPredicateExpression secondExpression) : base(firstExpression, secondExpression)
        {

        }

        public static XorExpression DataXorExpressionToXorExpression(DataXorExpression dataXorExpression)
        {
            return new XorExpression(
                IPredicateExpression.DataPredicateExpressionToIPredicateExpression(dataXorExpression.First),
                IPredicateExpression.DataPredicateExpressionToIPredicateExpression(dataXorExpression.Second)
                );
        }

        // if ([] and ![]) or (![] and []) 
        public override bool EvaluatePredicate(ShoppingBag bag, Store store)
        {
            return (firstExpression.EvaluatePredicate(bag, store) && !secondExpression.EvaluatePredicate(bag, store))
                || (secondExpression.EvaluatePredicate(bag, store) && !firstExpression.EvaluatePredicate(bag, store));
        }
    }
}
