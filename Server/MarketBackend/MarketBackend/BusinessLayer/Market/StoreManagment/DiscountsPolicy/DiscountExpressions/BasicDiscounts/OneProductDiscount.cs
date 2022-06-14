using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.BasicDiscounts;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.BasicDiscounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions
{
    public class OneProductDiscount : StoreDiscount
    {
        public int productId { get; set; }

        public OneProductDiscount(int productId, int discount) : base(discount)
        {
            this.productId = productId;
        }

        public static OneProductDiscount DataOneProductDiscountToOneProductDiscount(DataOneProductDiscount dataOneProductDiscount)
        {
            return new OneProductDiscount(dataOneProductDiscount.ProductId, dataOneProductDiscount.Discount);
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

        public override DataExpression IExpressionToDataExpression()
        {
            return new DataOneProductDiscount()
            {
                ProductId = productId,
                Discount = discount
            };
        }

        public override void RemoveFromDB(DataExpression de)
        {
            DataOneProductDiscount dopd = (DataOneProductDiscount)de; 
            //TODO myself
        }

    }
}
