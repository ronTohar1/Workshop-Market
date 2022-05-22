﻿using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts
{
    public class BagValuePredicate : IPredicateExpression
    {
        private int worth;
        
        public BagValuePredicate(int worth)
        {
            this.worth = worth;
        }

        //check of the bag worth more than worth
        public virtual bool EvaluatePredicate(ShoppingBag bag, Store store)
        {
            double sum = 0;
            IDictionary<int, Product> prods = store.products;
            foreach (ProductInBag pib in bag.ProductsAmounts.Keys)
            {
                sum += bag.ProductsAmounts[pib] * store.products[pib.ProductId].GetPrice();
            }
            return sum >= worth;
        }
    }
}