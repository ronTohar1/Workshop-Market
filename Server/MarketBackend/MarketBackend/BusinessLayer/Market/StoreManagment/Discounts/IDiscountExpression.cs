using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts
{
    internal interface IDiscountExpression
    {
        public int EvaluateDiscount(ShoppingBag bag);
        public bool EvaluatePredicate(ShoppingBag bag);
    }
}
