using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.RestrictionPolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies
{
    public class BeforeHourRestriction : AfterHourRestriction
    {
        public BeforeHourRestriction(int hour) : base(hour) { }

        public static BeforeHourRestriction DataBeforeHourRestrictionToBeforeHourRestriction(DataBeforeHourRestriction dataBeforeHourRestriction)
        {
            // BeforeHourRestriction or BeforeHourProductRestriction
            if (dataBeforeHourRestriction is DataBeforeHourProductRestriction)
                return BeforeHourProductRestriction.DataBeforeHourProductRestrictionToBeforeHourProductRestriction((DataBeforeHourProductRestriction)dataBeforeHourRestriction);
            else
                return new BeforeHourRestriction(dataBeforeHourRestriction.Hour); 
        }

        public override bool IsSatisfied(ShoppingBag bag)
        {
            return !base.IsSatisfied(bag);
        }

        public override DataIPurchasePolicy IPurchasePolicyToDataIPurchasePolicy()
        {
            return new DataBeforeHourRestriction()
            {
                Hour = hour
            };
        }

        public override void RemoveFromDB(DataIPurchasePolicy dpp)
        {
            DataBeforeHourRestriction dbhr = (DataBeforeHourRestriction)dpp;
            //TODO myself
        }
    }
}
