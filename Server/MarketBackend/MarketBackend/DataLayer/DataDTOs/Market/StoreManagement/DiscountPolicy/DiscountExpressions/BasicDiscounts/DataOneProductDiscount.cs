using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.BasicDiscounts
{
    public class DataOneProductDiscount : DataStoreDiscount
    {
        public int ProductId { get; set; }
    }
}
