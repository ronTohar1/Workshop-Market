using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Buyers.Carts
{
    public class DataCart
    {
        IDictionary<int, DataShoppingBag> ShoppingBags { get; set; }
        private IList<Purchase> purchaseHistory;
    }
}
