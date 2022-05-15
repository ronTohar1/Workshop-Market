using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.BasicDiscounts
{
    internal class StoreDiscount : IDiscountExpression
    {
        public int discount { get; set; }

        public StoreDiscount(int discount)
        {
            this.discount = discount;
        }

        //calculate the sum of the bag and return discount percent of it
        public virtual double EvaluateDiscount(ShoppingBag bag, Store store)
        {
            throw new NotImplementedException();
        }

        public double sumOfCart(ShoppingBag bag, Store store)
        {
            double sum = 0;
            foreach (ProductInBag pib in bag.ProductsAmounts.Keys)
            {
                sum += bag.ProductsAmounts[pib] * store.products[pib.ProductId].GetPrice();
            }
            return sum;
        }
    }
}
