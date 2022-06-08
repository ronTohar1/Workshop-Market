using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy
{
    public class DataDiscount
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DataExpression? DiscountExpression { get; set; }
    }
}
