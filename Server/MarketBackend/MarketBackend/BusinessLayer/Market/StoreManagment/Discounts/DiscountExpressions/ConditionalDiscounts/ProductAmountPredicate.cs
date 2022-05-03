using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts
{
    internal class ProductAmountPredicate : IDiscountExpression
    {
        private int id;
        private int quantity;

        public ProductAmountPredicate(int id, int quantity)
        {
            this.id = id;
            this.quantity = quantity;
        }

        public int EvaluateDiscount(ShoppingBag bag)
        {
            throw new NotImplementedException();
        }

        public bool EvaluatePredicate(ShoppingBag bag)
        {
            throw new NotImplementedException();
        }
    }
}
