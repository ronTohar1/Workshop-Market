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
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }

        public DateDiscount(int discount, int year, int month, int day) : base(discount)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }


        // if date then discount applies
        public override double EvaluateDiscount(ShoppingBag bag, Store store)
        {

            DateTime date = DateTime.Now;
            if (year == -1 || year == date.Year)
                if (month == -1 || month == date.Month)
                    if (day == -1 || day == date.Day)
                        return (sumOfCart(bag, store) * discount) / 100;
            return 0;
        }
    }
}
