﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    // r.6.4
    public class Purchase 
    {
        public  DateTime purchaseDate { get; }
        public double purchasePrice { get; }
        public string purchaseDescription { get; }
        public Purchase()
        {
           // for tests
        }
        public Purchase(DateTime purchaseDate, double purchasePrice, string purchaseDescription) { 
            this.purchaseDate = purchaseDate;   
            this.purchasePrice = purchasePrice;
            this.purchaseDescription = purchaseDescription;
        }
        public virtual string GetPurchaseDescription()
            => purchaseDescription;
            
                
    }
}