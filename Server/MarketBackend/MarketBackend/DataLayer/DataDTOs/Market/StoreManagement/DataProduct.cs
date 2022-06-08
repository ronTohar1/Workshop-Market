using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs
{
    public class DataProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public int AmountInInventory { get; set; }
		public double PricePerUnit { get; set; }
		public string Category { get; set; }
		public double Productdicount { get; set; }
		public IList<DataProductReview> Reviews { get; set; }
		public IList<DataPurchaseOption> PurchaseOptions { get; set; }
	}
}
