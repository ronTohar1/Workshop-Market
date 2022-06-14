using MarketBackend.DataLayer.DataDTOs.Market;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    // r.6.4
    public class Purchase
    {
        public virtual DateTime purchaseDate { get; }
        public virtual double purchasePrice { get; }
        public virtual string purchaseDescription { get; }

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

        // r S 8
        public static Purchase DataPurchaseToPurchase(DataPurchase dataPurchase)
        {
            return new Purchase(dataPurchase.BuyerId, dataPurchase.PurchaseDate, dataPurchase.PurchasePrice, 
                dataPurchase.PurchaseDescription); 
        }

        public DataPurchase ToNewDataPurchase(DataStore dataStore)
        {
            return new DataPurchase()
            {
                BuyerId = this.BuyerId,
                Store = dataStore,
                PurchaseDate = this.purchaseDate,
                PurchasePrice = this.purchasePrice,
                PurchaseDescription = this.purchaseDescription
            }; 
        }

        public virtual string GetPurchaseDescription()
            => purchaseDescription;


    }
}