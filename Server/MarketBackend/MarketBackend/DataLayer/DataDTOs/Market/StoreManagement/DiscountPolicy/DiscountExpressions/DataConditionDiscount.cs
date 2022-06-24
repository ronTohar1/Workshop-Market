using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions
{
    public class DataConditionDiscount : DataConditionalExpression
    {
        public virtual DataPredicateExpression? Predicate { get; set; }
        public virtual DataDiscountExpression? DiscountExpression { get; set; }
    }
}
