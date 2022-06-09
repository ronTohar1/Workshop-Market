using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataDTOs.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Buyers
{
    public class DataMember
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Password { get; set; }
        public IList<DataNotification> PendingNotifications { get; set; }

        public bool IsAdmin { get; set; }
        
        // buyer data

        public DataCart? Cart { get; set; }
        public IList<DataPurchase> PurchaseHistory { get; set; }
    }
}
