using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.NumericExpressions
{
    public class MaxExpression : IDiscountExpression
    {
        IList<IDiscountExpression> discounts;
        public MaxExpression()
        {
            discounts = new List<IDiscountExpression>();
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
