using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.BasicDiscounts
{
    public class DateDiscount : StoreDiscount
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
            if (IsAllowedDiscount(date.Year, date.Month, date.Day))
                return (GetSum(bag, store) * discount) / 100;
            return 0;
        }

        private bool IsAllowedDiscount(int year, int month, int day)
        {
            if (this.year == -1 || this.year == year)
                if (this.month == -1 || this.month == month)
                    if (this.day == -1 || this.day == day)
                        return true;
            return false;
        }

        public virtual double GetSum(ShoppingBag bag, Store store)
        {
            return sumOfCart(bag, store);
        }
    }
}
