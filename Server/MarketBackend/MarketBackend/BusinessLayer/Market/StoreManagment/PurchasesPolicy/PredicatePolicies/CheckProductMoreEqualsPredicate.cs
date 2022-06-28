using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PredicatePolicies;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces;


namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PredicatePolicies
{
    public class CheckProductMoreEqualsPredicate : CheckProductLessPredicate
    {
        public CheckProductMoreEqualsPredicate(int productId, int amount ) : base(productId, amount) { }

        public static CheckProductMoreEqualsPredicate DataCheckProductMoreEqualsPredicateToCheckProductMoreEqualsPredicate(DataCheckProductMoreEqualsPredicate dataCheckProductMoreEqualsPredicate)
        {
            return new CheckProductMoreEqualsPredicate(dataCheckProductMoreEqualsPredicate.ProductId, dataCheckProductMoreEqualsPredicate.Amount); 
        }

        public override bool IsSatisfied(ShoppingBag bag)
        {
            return !(base.IsSatisfied(bag));
        }

        public override DataPredicateExpression PredicateExpressionToDataPredicateExpression()
        {
            return new DataCheckProductMoreEqualsPredicate()
            {
                ProductId = this.productId,
                Amount = this.amount
            };
        }

        public override void RemoveFromDB(DataPredicateExpression dpe)
        {
            DataCheckProductMoreEqualsPredicate dcplp = (DataCheckProductMoreEqualsPredicate)dpe;
            //TODO myself
        }
    }
}
