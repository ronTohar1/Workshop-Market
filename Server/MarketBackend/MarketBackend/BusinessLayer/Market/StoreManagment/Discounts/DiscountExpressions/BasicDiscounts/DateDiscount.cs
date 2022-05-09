using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.BasicDiscounts
{
    internal class DateDiscount : StoreDiscount
    {
        public DateTime date { get; set; }

        public DateDiscount(DateTime date, int discount) : base(discount)
        {
            this.date = date;
        }

        //check if its the right date
        public override int EvaluateDiscount(ShoppingBag bag)
        {
            throw new NotImplementedException();
        }
    }
}
