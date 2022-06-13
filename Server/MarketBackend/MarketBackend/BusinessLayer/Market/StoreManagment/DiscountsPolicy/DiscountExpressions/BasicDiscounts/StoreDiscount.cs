using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.BasicDiscounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.BasicDiscounts
{
    public class StoreDiscount : IDiscountExpression
    {
        public int discount { get; set; }

        public StoreDiscount(int discount)
        {
            this.discount = discount;
        }

        //calculate the sum of the bag and return discount percent of it
        public virtual double EvaluateDiscount(ShoppingBag bag, Store store)
        {
            return (sumOfCart(bag, store) * discount)/100;
        }

        public virtual double sumOfCart(ShoppingBag bag, Store store)
        {
            double sum = 0;
            foreach (ProductInBag pib in bag.ProductsAmounts.Keys)
            {
                sum += bag.ProductsAmounts[pib] * store.products[pib.ProductId].GetPrice();
            }
            return sum;
        }

        public static StoreDiscount DataStoreDiscountToStoreDiscount(DataStoreDiscount dataStoreDiscount)
        {
            // StoreDiscount or DateDiscount or OneProductDiscount

            if (dataStoreDiscount is DataDateDiscount)
                return DateDiscount.DataDateDiscountToDateDiscount((DataDateDiscount)dataStoreDiscount);
            else if (dataStoreDiscount is DataOneProductDiscount)
                return OneProductDiscount.DataOneProductDiscountToOneProductDiscount((DataOneProductDiscount)dataStoreDiscount);
            return new StoreDiscount(dataStoreDiscount.Discount); 
        }

        public virtual DataExpression IExpressionToDataExpression()
        {
            return new DataStoreDiscount()
            {
                Discount = this.discount
            };
        }

        public virtual void RemoveFromDB()
        {
            //TODO myself
        }
    }
}
