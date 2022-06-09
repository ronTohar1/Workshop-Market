using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Buyers.Carts
{
    public class DataShoppingBag
    {
        public int Id { get; set; }
        public DataStore? Store { get; set; }
        public IList<DataProductInBag> ProductsAmounts { get; set; }
    }
}
