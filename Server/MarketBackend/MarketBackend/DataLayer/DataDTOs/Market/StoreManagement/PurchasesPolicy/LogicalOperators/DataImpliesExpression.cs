using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.LogicalOperators
{
    public class DataImpliesExpression : DataIPurchasePolicy
    {
        public virtual DataPredicateExpression? Condition { get; set; }
        public virtual DataPredicateExpression? Allowing { get; set; }
    }
}
