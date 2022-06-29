using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy
{
    public class DataStoreDiscountPolicyManager
    {
        public int Id { get; set; }
        public virtual IList<DataDiscount?>? Discounts { get; set; }
    }
}
