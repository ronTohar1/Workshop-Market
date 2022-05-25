using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies
{
    public class DateRestriction : IRestrictionExpression
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }

        //cant buy on that date
        public DateRestriction(int year, int month, int day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }

        public bool IsSatisfied(ShoppingBag bag)
        {
            DateTime now = DateTime.Now;
            bool cond1 = (year == -1) || (year == now.Year);
            bool cond2 = (month == -1) || (month == now.Month);
            bool cond3 = (day == -1) || (day == now.Day);
            return !(cond1 && cond2 && cond3);
        }
    }
}
