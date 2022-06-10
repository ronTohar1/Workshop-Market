using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy
{
    public class DataPurchasePolicy
    {
        public int Id { get; set; }
        public DataPurchasePolicy? Policy { get; set; }
        public string Description { get; set; }
    }
}
