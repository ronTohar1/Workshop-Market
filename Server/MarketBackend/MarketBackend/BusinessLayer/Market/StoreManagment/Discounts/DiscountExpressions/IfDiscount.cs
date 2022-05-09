using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions
{
    internal class IfDiscount : IConditionalExpression
    {
        private IPredicateExpression test;
        private IDiscountExpression thenDis;
        private IDiscountExpression elseDis;

        public IfDiscount(IPredicateExpression test, IDiscountExpression thenDis, IDiscountExpression elseDis)
        {
            this.test = test;
            this.thenDis = thenDis;
            this.elseDis = elseDis;
        }

        public int EvaluateDiscount(ShoppingBag bag)
        {
            if (test.EvaluatePredicate(bag))
                return thenDis.EvaluateDiscount(bag);
            return elseDis.EvaluateDiscount(bag);
        }
    }
}
