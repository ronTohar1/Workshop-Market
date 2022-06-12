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
        public DataPredicateExpression? Predicate { get; set; }
        public DataDiscountExpression? DiscountExpression { get; set; }
    }
}
