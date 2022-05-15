using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.BasicDiscounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions
{
    internal class OneProductDiscount : StoreDiscount
    {
        public int productId { get; set; }

        public OneProductDiscount(int productId, int discount) : base(discount)
        {
            this.productId = productId;
        }

        //check the price of the certain product id in the bag and return the discount percent of it
        public override double EvaluateDiscount(ShoppingBag bag, Store store)
        {
            foreach (ProductInBag pib in bag.ProductsAmounts.Keys)
            {
                if (pib.ProductId == productId)
                {
                    return ((bag.ProductsAmounts[pib] * store.products[productId].GetPrice())*discount)/100;
                }
            }
            return 0;
        }

    }
}
