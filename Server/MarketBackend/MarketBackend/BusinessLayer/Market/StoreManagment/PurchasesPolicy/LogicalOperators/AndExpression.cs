using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.logicalOperators
{
    internal class AndExpression : IPurchasePolicy
    {
        public IRestrictionExpression firstPred { get; set; }
        public IRestrictionExpression secondPred { get; set; }

        public AndExpression(IRestrictionExpression firstPred, IRestrictionExpression secondPred)
        {
            this.firstPred = firstPred;
            this.secondPred = secondPred;
        }

        public static AndExpression DataAndExpressionToAndExpression(DataAndExpression dataAndExpression)
        {
            return new AndExpression(
                IRestrictionExpression.DataRestrictionExpressionToIRestrictionExpression(dataAndExpression.FirstPred),
                IRestrictionExpression.DataRestrictionExpressionToIRestrictionExpression(dataAndExpression.SecondPred)
                );
        }

        public bool IsSatisfied(ShoppingBag bag)
        {
            return firstPred.IsSatisfied(bag) && secondPred.IsSatisfied(bag);
        }

        public DataIPurchasePolicy IPurchasePolicyToDataIPurchasePolicy()
        {
            return new DataAndExpression()
            {
                FirstPred = (DataRestrictionExpression)firstPred.IPurchasePolicyToDataIPurchasePolicy(),
                SecondPred = (DataRestrictionExpression)secondPred.IPurchasePolicyToDataIPurchasePolicy()
            };
        }

        public void RemoveFromDB(DataIPurchasePolicy dpp)
        {
            DataAndExpression dae = (DataAndExpression)dpp;
            firstPred.RemoveFromDB(dae.FirstPred);
            secondPred.RemoveFromDB(dae.SecondPred);
            //TODO myself
        }
    }
}
