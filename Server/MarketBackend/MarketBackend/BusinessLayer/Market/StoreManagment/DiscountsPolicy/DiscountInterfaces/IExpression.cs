using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts
{
    public interface IExpression
    {
        public double EvaluateDiscount(ShoppingBag bag, Store store);

        public DataExpression IExpressionToDataExpression();

        public void RemoveFromDB(DataExpression de);

        public static IExpression DataExpressionToIExpression(DataExpression dataExpression)
        {
            // IDiscountExpression or ContidionalExpression

            if (dataExpression is DataDiscountExpression)
                return IDiscountExpression.DataDiscountExpressionToIDiscountExpression((DataDiscountExpression)dataExpression);
            else if (dataExpression is DataConditionalExpression)
                return IConditionalExpression.DataConditionalExpressionToIConditionalExpression((DataConditionalExpression)dataExpression); 
            else
                throw new Exception("not supporting this inherent of IExpression"); 
        }
    }
}
