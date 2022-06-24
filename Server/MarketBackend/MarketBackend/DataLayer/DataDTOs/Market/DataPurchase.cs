using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketBackend.DataLayer.DataDTOs.Market
{
    public class DataPurchase
    {
        public int Id { get; set; }
        public int BuyerId { get; set; } 
        [ForeignKey("DataStore")]
        public virtual DataStore? Store { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double PurchasePrice { get; set; }
        public string PurchaseDescription { get; set; }
    }
}
