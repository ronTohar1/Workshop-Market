using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
