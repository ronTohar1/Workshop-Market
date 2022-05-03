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
        public IDiscountExpression firstExpression { get; set; }
        public IDiscountExpression secondExpression { get; set; }

        public LogicalExpression(IDiscountExpression firstExpression, IDiscountExpression secondExpression)
        {
            this.firstExpression = firstExpression;
            this.secondExpression = secondExpression;
        }

        public virtual int EvaluateDiscount(ShoppingBag bag)
        {
            throw new NotImplementedException();
        }

        public virtual bool EvaluatePredicate(ShoppingBag bag)
        {
            throw new NotImplementedException();
        }

    }
}
