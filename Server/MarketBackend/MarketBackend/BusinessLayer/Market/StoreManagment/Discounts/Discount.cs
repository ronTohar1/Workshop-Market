using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators;
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

        public IExpression discountExpression { get; set; }

        public Discount(int id, string description, IExpression discountExpression)
        {
            this.id = id;
            this.description = description;
            this.discountExpression = discountExpression;
        }

    }
}
