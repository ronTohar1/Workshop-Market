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
        public DateTime purchaseDate { get; }
        public double purchasePrice { get; }
        public string purchaseDescription { get; }

        public int BuyerId { get; }
        public Purchase()
        {
            // for tests
        }
        public Purchase(int buyerId, DateTime purchaseDate, double purchasePrice, string purchaseDescription)
        {
            this.purchaseDate = purchaseDate;
            this.purchasePrice = purchasePrice;
            this.purchaseDescription = purchaseDescription;
            this.BuyerId = buyerId;
        }
        public virtual string GetPurchaseDescription()
            => purchaseDescription;


    }
}