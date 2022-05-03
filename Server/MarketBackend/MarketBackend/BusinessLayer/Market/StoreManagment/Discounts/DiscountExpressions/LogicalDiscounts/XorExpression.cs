using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalDiscounts
{
    internal class XorExpression : LogicalExpression
    {
        public XorExpression(IDiscountExpression firstExpression, IDiscountExpression secondExpression) : base(firstExpression, secondExpression)
        {

        }
    }
}
