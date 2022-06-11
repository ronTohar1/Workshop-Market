using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.RestrictionPolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces
{
    public interface IRestrictionExpression : IPurchasePolicy
    {
        public static IRestrictionExpression DataRestrictionExpressionToIRestrictionExpression(DataRestrictionExpression dataRestrictionExpression)
        {
            // AfterHourRestriction or AtlestAmountRestriction or DateRestriction

            if (dataRestrictionExpression is DataAfterHourRestriction)
                return AfterHourRestriction.DataAfterHourRestrictionToAfterHourRestriction((DataAfterHourRestriction)dataRestrictionExpression); 
            else if (dataRestrictionExpression is DataAtLeastAmountRestriction)
                return AtlestAmountRestriction.DataAtlestAmountRestrictionToAtlestAmountRestriction((DataAtLeastAmountRestriction)dataRestrictionExpression);
            else if (dataRestrictionExpression is DataDateRestriction)
                return DateRestriction.DataDateRestrictionToDateRestriction((DataDateRestriction)dataRestrictionExpression); 
            else
                throw new Exception("not supporting this inherent of IRestrictionExpression"); 
        }
    }
}
