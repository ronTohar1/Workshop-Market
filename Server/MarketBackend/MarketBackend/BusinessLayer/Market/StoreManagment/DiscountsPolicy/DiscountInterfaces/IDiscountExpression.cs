using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.BasicDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.NumericExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.BasicDiscounts;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.NumericExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts
{
    public interface IDiscountExpression : IExpression
    {
        public static IDiscountExpression DataDiscountExpressionToIDiscountExpression(DataDiscountExpression dataDiscountExpression)
        {
            // StoreDiscount or MaxExpression 

            if (dataDiscountExpression is DataStoreDiscount)
                return StoreDiscount.DataStoreDiscountToStoreDiscount((DataStoreDiscount)dataDiscountExpression);
            else if (dataDiscountExpression is DataMaxExpression)
                return MaxExpression.DataMaxExpressionToMaxExpression((DataMaxExpression)dataDiscountExpression); 
            else
                throw new Exception("not supporting this inherent of IDiscountExpression");
        }
    }
}
