using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalDiscounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts
{
    internal class StoreDiscountManager
    {
        private IList<Discount> discounts;

        public StoreDiscountManager()
        {
            this.discounts = new List<Discount>();
        }

        public IDiscountExpression newAndExpression(IDiscountExpression ex1, IDiscountExpression ex2)
        {
            IDiscountExpression newExp = new AndExpression(ex1, ex2);
            return newExp;
        }

        public IDiscountExpression newXorExpression(IDiscountExpression ex1, IDiscountExpression ex2)
        {
            IDiscountExpression newExp = new XorExpression(ex1, ex2);
            return newExp;
        }

        public IDiscountExpression newOrExpression(IDiscountExpression ex1, IDiscountExpression ex2)
        {
            IDiscountExpression newExp = new OrExpression(ex1, ex2);
            return newExp;
        }
    }
}
