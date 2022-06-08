using MarketBackend.BusinessLayer.Market.StoreManagment;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketBackend.DataLayer.DataDTOs
{
    public class DataPurchaseOption
    {
        public int Id { get; set; }
        public PurchaseOption PurchaseOption { get; set; }
    }
}
