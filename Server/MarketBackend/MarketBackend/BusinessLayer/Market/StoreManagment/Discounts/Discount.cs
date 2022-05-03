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
        public int id { get; set; }
        public string description { get; set; }

        public IDiscountExpression discountExpression { get; set; }

        public Discount(int id, string description, IDiscountExpression discountExpression)
        {
            this.id = id;
            this.description = description;
            this.discountExpression = discountExpression;
        }

    }
}
