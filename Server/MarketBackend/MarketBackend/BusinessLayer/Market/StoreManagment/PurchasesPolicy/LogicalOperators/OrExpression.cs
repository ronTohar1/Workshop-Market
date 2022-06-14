using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.logicalOperators
{
    public class OrExpression : IPurchasePolicy
    {
        public IRestrictionExpression firstPred { get; set; }
        public IRestrictionExpression secondPred { get; set; }

        public OrExpression(IRestrictionExpression firstPred, IRestrictionExpression secondPred)
        {
            this.firstPred = firstPred;
            this.secondPred = secondPred;
        }

        public static OrExpression DataOrExpressionToOrExpression(DataOrExpression dataOrExpression)
        {
            return new OrExpression(
                IRestrictionExpression.DataRestrictionExpressionToIRestrictionExpression(dataOrExpression.FirstPred),
                IRestrictionExpression.DataRestrictionExpressionToIRestrictionExpression(dataOrExpression.SecondPred)
                );
        }

        public bool IsSatisfied(ShoppingBag bag)
        {
            return firstPred.IsSatisfied(bag) || secondPred.IsSatisfied(bag);
        }

        public DataIPurchasePolicy IPurchasePolicyToDataIPurchasePolicy()
        {
            return new DataOrExpression()
            {
                FirstPred = (DataRestrictionExpression)firstPred.IPurchasePolicyToDataIPurchasePolicy(),
                SecondPred = (DataRestrictionExpression)secondPred.IPurchasePolicyToDataIPurchasePolicy()
            };
        }

        public void RemoveFromDB(DataIPurchasePolicy dpp)
        {
            DataOrExpression doe = (DataOrExpression)dpp;
            firstPred.RemoveFromDB(doe.FirstPred);
            secondPred.RemoveFromDB(doe.SecondPred);
            //TODO myself
        }
    }
}
