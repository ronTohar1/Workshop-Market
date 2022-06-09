using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.NumericExpressions
{
    public class AddativeExpression : IDiscountExpression
    {
        public IList<IDiscountExpression> discounts { get; set; }
        public AddativeExpression()
        {
            discounts = new List<IDiscountExpression>();
        }

        public void AddDiscount(IDiscountExpression discount)
        {
            discounts.Add(discount);
        }

        public double EvaluateDiscount(ShoppingBag bag, Store store)
        {
            double sumDis = 0;
            for (int i = 0; i < discounts.Count; i++)
            {
                sumDis = sumDis+ discounts[i].EvaluateDiscount(bag, store);
            }
            return sumDis;
        }
    }
}
