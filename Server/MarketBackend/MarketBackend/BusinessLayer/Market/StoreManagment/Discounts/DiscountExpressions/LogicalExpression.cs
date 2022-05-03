using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions
{
    internal abstract class LogicalExpression : IDiscountExpression
    {
        private IDiscountExpression firstExpression;
        private IDiscountExpression secondExpression;

        public LogicalExpression(IDiscountExpression firstExpression, IDiscountExpression secondExpression)
        {
            this.firstExpression = firstExpression;
            this.secondExpression = secondExpression;
        }

        public IDiscountExpression GetFirstExpression()
        {
            return firstExpression;
        }

        public IDiscountExpression GetSecondExpression()
        {
            return secondExpression;
        }

        public int CalcDiscount(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
