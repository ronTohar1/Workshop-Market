using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.RestrictionPolicies;
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

        public static AfterHourRestriction DataAfterHourRestrictionToAfterHourRestriction(DataAfterHourRestriction dataAfterHourRestriction)
        {
            // AfterHourRestriction or AfterHourProductRestriction or BeforeHourRestrcition

            if (dataAfterHourRestriction is DataAfterHourProductRestriction)
                return AfterHourProductRestriction.DataAfterHourProductRestrictionToAfterHourProductRestriction((DataAfterHourProductRestriction)dataAfterHourRestriction); 
            else if (dataAfterHourRestriction is DataBeforeHourRestriction)
                return BeforeHourRestriction.DataBeforeHourRestrictionToBeforeHourRestriction((DataBeforeHourRestriction)dataAfterHourRestriction);
            else 
                return new AfterHourRestriction(dataAfterHourRestriction.Hour);
        }

        public virtual bool IsSatisfied(ShoppingBag bag)
        {
            return DateTime.Now.Hour <= hour;
        }
    }
}
