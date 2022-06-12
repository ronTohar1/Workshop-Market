using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.LogicalOperators
{
    internal class ImpliesExpression : IPurchasePolicy
    {
        //condition is what is allowed only if a different things happens
        public IPredicateExpression condition { get; set; }

        //allowing is the thing condition is dependent on
        public IPredicateExpression allowing { get; set; }

        //example:
        //
        // "you can buy a potato only if you have bread in your cart"
        // condition = has a potato in the cart
        // allowing = has bread in the cart
        //
        // so if condition true allowing has to be true
        // if condition false it does not matter what allowing is 


        public ImpliesExpression(IPredicateExpression condition, IPredicateExpression allowing)
        {
            this.condition = condition;
            this.allowing = allowing;
        }

        public static ImpliesExpression DataImpliesExpressionToImpliesExpression(DataImpliesExpression dataImpliesExpression)
        {
            return new ImpliesExpression(
                IPredicateExpression.DataPredicateExpressionToIPredicateExpression(dataImpliesExpression.Condition),
                IPredicateExpression.DataPredicateExpressionToIPredicateExpression(dataImpliesExpression.Allowing)
                );
        }

        public bool IsSatisfied(ShoppingBag bag)
        {
            bool cond = condition.IsSatisfied(bag);
            if (cond)
                return allowing.IsSatisfied(bag);
            else
                return true;
        }
    }
}
