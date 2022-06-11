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
    }
}
