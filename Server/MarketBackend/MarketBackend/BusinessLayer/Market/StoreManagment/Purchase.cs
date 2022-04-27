using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class Purchase
    {
        private DateTime purchaseDate { get; }
        private double purchasePrice { get; }
        private string purchaseDescription { get; }
        public Purchase(DateTime purchaseDate, double purchasePrice, string purchaseDescription) { 
            this.purchaseDate = purchaseDate;   
            this.purchasePrice = purchasePrice;
            this.purchaseDescription = purchaseDescription;
        }
        
                
    }
}
