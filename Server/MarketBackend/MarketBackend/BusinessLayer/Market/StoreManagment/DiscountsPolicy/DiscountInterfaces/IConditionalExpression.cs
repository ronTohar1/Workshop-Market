using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts
{
    public interface IConditionalExpression : IExpression
    {
        public static IConditionalExpression DataConditionalExpressionToIConditionalExpression(DataConditionalExpression dataConditionalExpression)
        {
            // ConditionDiscount or IfDiscount
            // 
            if (dataConditionalExpression is DataConditionDiscount)
                return ConditionDiscount.DataConditionDiscountToConditionDiscount((DataConditionDiscount)dataConditionalExpression); 
            else if (dataConditionalExpression is DataIfDiscount)
                return IfDiscount.DataIfDiscountToIfDiscount((DataIfDiscount)dataConditionalExpression); 
            else
                throw new Exception("not supporting this inherent of IConditionalExpression");
        }
    }
}
