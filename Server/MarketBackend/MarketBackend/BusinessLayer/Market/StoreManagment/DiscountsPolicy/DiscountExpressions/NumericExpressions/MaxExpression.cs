using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.NumericExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.NumericExpressions
{
    public class MaxExpression : IDiscountExpression
    {
        public IList<IDiscountExpression> discounts { get; set; }
        public MaxExpression() : this(new List<IDiscountExpression>())
        {
        }

        private MaxExpression(IList<IDiscountExpression> discounts)
        {
            this.discounts = discounts;
        }

        public static MaxExpression DataMaxExpressionToMaxExpression(DataMaxExpression dataMaxExpression)
        {
            IList<IDiscountExpression> discounts = new List<IDiscountExpression>(); 

            foreach (DataDiscountExpression dataDiscountExpression in dataMaxExpression.Discounts)
            {
                discounts.Add(IDiscountExpression.DataDiscountExpressionToIDiscountExpression(dataDiscountExpression));
            }

            return new MaxExpression(discounts); 
        }

        public void AddDiscount(IDiscountExpression discount)
        {
            discounts.Add(discount);
        }

        public double EvaluateDiscount(ShoppingBag bag, Store store)
        {
            double maxDis = 0;
            for (int i = 0; i < discounts.Count; i++)
            {
                double newDis = discounts[i].EvaluateDiscount(bag, store);
                if (newDis > maxDis)
                {
                    maxDis = newDis;
                }
            }
            return maxDis;
        }
    }
}
