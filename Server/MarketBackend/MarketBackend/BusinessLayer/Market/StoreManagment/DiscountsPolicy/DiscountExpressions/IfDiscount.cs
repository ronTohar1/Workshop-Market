using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions
{
    public class IfDiscount : IConditionalExpression
    {
        private IPredicateExpression test;
        private IDiscountExpression thenDis;
        private IDiscountExpression elseDis;

        public IfDiscount(IPredicateExpression test, IDiscountExpression thenDis, IDiscountExpression elseDis)
        {
            this.test = test;
            this.thenDis = thenDis;
            this.elseDis = elseDis;
        }

        public static IfDiscount DataIfDiscountToIfDiscount(DataIfDiscount dataIfDiscount)
        {
            return new IfDiscount(
                IPredicateExpression.DataPredicateExpressionToIPredicateExpression(dataIfDiscount.Test),  
                IDiscountExpression.DataDiscountExpressionToIDiscountExpression(dataIfDiscount.Then), 
                IDiscountExpression.DataDiscountExpressionToIDiscountExpression(dataIfDiscount.Else)
                ); 
        }

        //if [] then [] else []
        public double EvaluateDiscount(ShoppingBag bag, Store store)
        {
            if (test.EvaluatePredicate(bag, store))
                return thenDis.EvaluateDiscount(bag, store);
            return elseDis.EvaluateDiscount(bag, store);
        }

        public DataExpression IExpressionToDataExpression()
        {
            return new DataIfDiscount()
            {
                Test = test.IPredicateExpressionToDataPredicateExpression(),
                Then = (DataDiscountExpression)thenDis.IExpressionToDataExpression(),
                Else = (DataDiscountExpression)elseDis.IExpressionToDataExpression()
            };
        }

        public void RemoveFromDB()
        {
            test.RemoveFromDB();
            thenDis.RemoveFromDB();
            elseDis.RemoveFromDB();
            // TODO myself
        }
    }
}

//if ((A or B) and (C or D)) then discount else discount 