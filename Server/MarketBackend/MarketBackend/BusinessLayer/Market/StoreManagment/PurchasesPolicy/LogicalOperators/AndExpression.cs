using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
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

        public bool IsSatisfied(ShoppingBag bag)
        {
            return firstPred.IsSatisfied(bag) && secondPred.IsSatisfied(bag);
        }
    }
}
