using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.NumericExpressions
{
    public class DataMaxExpression : DataDiscountExpression
    {
        public virtual IList<DataDiscountExpression?>? Discounts { get; set; }
    }
}
