using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.LogicalDiscounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalDiscounts
{
    public class OrDiscount : LogicalDiscount
    {
        public OrDiscount(IDiscountExpression firstExpression, IDiscountExpression secondExpression) : base(firstExpression, secondExpression)
        {

        }

        //should be overrided
        public double EvaluateDiscount(ShoppingBag bag, Store store)
        => this.firstDiscount.EvaluateDiscount(bag, store) + this.secondDiscount.EvaluateDiscount(bag, store);
    }
}
