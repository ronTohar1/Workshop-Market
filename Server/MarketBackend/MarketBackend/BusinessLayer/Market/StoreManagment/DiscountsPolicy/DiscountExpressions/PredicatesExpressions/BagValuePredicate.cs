using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.PredicatesExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts
{
    public class BagValuePredicate : IPredicateExpression
    {
        private int worth;
        
        public BagValuePredicate(int worth)
        {
            this.worth = worth;
        }

        public static BagValuePredicate DataBagValuePredicateToBagValuePredicate(DataBagValuePredicate dataBagValuePredicate)
        {
            return new BagValuePredicate(dataBagValuePredicate.Worth); 
        }

        //check of the bag worth more than worth
        public virtual bool EvaluatePredicate(ShoppingBag bag, Store store)
        {
            double sum = 0;
            IDictionary<int, Product> prods = store.products;
            foreach (ProductInBag pib in bag.ProductsAmounts.Keys)
            {
                int amount = bag.ProductsAmounts[pib];
                double price = prods[pib.ProductId].GetPrice();
                sum += amount * price;
            }
            return sum >= worth;
        }

        public DataPredicateExpression IPredicateExpressionToDataPredicateExpression()
        {
            return new DataBagValuePredicate()
            {
                Worth = worth
            };
        }

        public void RemoveFromDB(DataPredicateExpression dpe)
        {
            DataBagValuePredicate dbvp = (DataBagValuePredicate)dpe;
           //TODO myself
        }
    }
}
