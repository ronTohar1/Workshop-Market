using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies
{
    public class AfterHourRestriction : IRestrictionExpression
    {
        public int hour { get; set; }

        public AfterHourRestriction(int hour)
        {
            this.hour = hour;
        }

        public virtual bool IsSatisfied(ShoppingBag bag)
        {
            return DateTime.Now.Hour <= hour;
        }
    }
}
