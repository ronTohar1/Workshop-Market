using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Market.StoreManagment;
namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServicePurchase
    {
        public DateTime purchaseDate { get; }
        public double purchasePrice { get; }
        public string purchaseDescription { get; }
        public ServicePurchase(Purchase purchase) {
            if (purchase != null)
            {
                this.purchaseDate = purchase.purchaseDate;
                this.purchasePrice = purchase.purchasePrice;
                this.purchaseDescription = purchase.purchaseDescription;
            }
        }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                ServicePurchase p = (ServicePurchase)obj;
                return (purchaseDate.Equals(p.purchaseDate)) && (purchasePrice==p.purchasePrice) && (purchaseDescription.Equals(p.purchaseDescription));
            }
        }
    }
}
