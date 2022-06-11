using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.logicalOperators;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.LogicalOperators;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces
{
    public interface IPurchasePolicy
    {
        // return true if there is no problem, and false if the purchse cant be made.
        public bool IsSatisfied(ShoppingBag bag);

        public static IPurchasePolicy DataIPurchasePolicyToIPurchasePolicy(DataIPurchasePolicy dataIPurchasePolicy)
        {
            // And or Implies or Or or IRestrictionExpression

            if (dataIPurchasePolicy is DataAndExpression)
                return AndExpression.DataAndExpressionToAndExpression((DataAndExpression)dataIPurchasePolicy); 
            else if (dataIPurchasePolicy is DataImpliesExpression)
                return ImpliesExpression.DataImpliesExpressionToImpliesExpression((DataImpliesExpression)dataIPurchasePolicy);
            else if (dataIPurchasePolicy is DataOrExpression)
                return OrExpression.DataOrExpressionToOrExpression((DataOrExpression)dataIPurchasePolicy);
            else if (dataIPurchasePolicy is DataRestrictionExpression)
                return IRestrictionExpression.DataRestrictionExpressionToIRestrictionExpression((DataRestrictionExpression)dataIPurchasePolicy);
            else
                throw new Exception("not supporting this inherent of IPurchasePolicy"); 
        }
    }
}
