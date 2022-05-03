using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions
{
    internal abstract class NumericExpression : IDiscountExpression
    {
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
