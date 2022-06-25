using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy
{
    public class DataStorePurchasePolicyManager
    {
        public int Id { get; set; }
        public virtual IList<DataPurchasePolicy?>? PurchasesPolicies { get; set; }
    }
}
