using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalDiscounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts
{
    internal class Discount
    {
        private string description;
        private IDiscountExpression discountExpression;

        public Discount(string description, IDiscountExpression discountExpression)
        {
            this.description = description;
            this.discountExpression = discountExpression;
        }

        public void setDiscount(IDiscountExpression newDiscount)
        {
            discountExpression = newDiscount;
        }

        public IDiscountExpression GetDiscount() 
        {
            return discountExpression; 
        }

    }
}
