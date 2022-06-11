using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.PredicatesExpressions;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts
{
    public class ProductAmountPredicate : IPredicateExpression
    {
        public int pid { get; set; }
        public int quantity { get; set; } 

        public ProductAmountPredicate(int pid, int quantity)
        {
            this.pid = pid;
            this.quantity = quantity;
        }

        public static ProductAmountPredicate DataProductAmountPredicateToProductAmountPredicate(DataProductAmountPredicate dataProductAmountPredicate)
        {
            return new ProductAmountPredicate(dataProductAmountPredicate.ProductId, dataProductAmountPredicate.Quantity); 
        }

        // check if there is atleast quantity of pid 
        public bool EvaluatePredicate(ShoppingBag bag, Store store)
        {
            foreach (ProductInBag pib in bag.ProductsAmounts.Keys)
            {
                if (pib.ProductId == pid)
                    return bag.ProductsAmounts[pib] >= quantity;
            }
            return false;
        }
    }
}
