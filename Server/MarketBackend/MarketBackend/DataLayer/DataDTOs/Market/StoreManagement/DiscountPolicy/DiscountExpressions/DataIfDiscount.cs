using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions
{
    public class DataIfDiscount : DataConditionalExpression
    {
        public DataPredicateExpression? Test { get; set; }
        public DataDiscountExpression? Then { get; set; }
        public DataDiscountExpression? Else { get; set; }
    }
}
