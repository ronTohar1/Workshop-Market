using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PredicatePolicies;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PredicatePolicies;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces
{
    public interface IPredicateExpression
    {
        public bool IsSatisfied(ShoppingBag bag);

        public static IPredicateExpression DataPredicateExpressionToIPredicateExpression(DataPredicateExpression dataPredicateExpression)
        {
            // CheckProductLessPredicate

            if (dataPredicateExpression is DataCheckProductLessPredicate)
                return CheckProductLessPredicate.DataCheckProductLessPredicateToCheckProductLessPredicate((DataCheckProductLessPredicate)dataPredicateExpression); 
            else
                throw new Exception("not supporting this inherent of IPredicateExpression");
        }
    }
}
