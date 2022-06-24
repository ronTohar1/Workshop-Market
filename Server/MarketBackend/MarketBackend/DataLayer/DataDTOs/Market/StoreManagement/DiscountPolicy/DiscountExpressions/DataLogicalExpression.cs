using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions
{
    public abstract class DataLogicalExpression : DataPredicateExpression
    {
        public virtual DataPredicateExpression? First { get; set; }
        public virtual DataPredicateExpression? Second { get; set; }
    }
}
