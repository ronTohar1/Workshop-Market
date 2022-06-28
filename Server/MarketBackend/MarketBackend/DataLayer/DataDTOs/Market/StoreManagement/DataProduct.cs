using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketBackend.DataLayer.DataDTOs
{
    public class DataProduct
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
		public string Name { get; set; }
		public int AmountInInventory { get; set; }
		public double PricePerUnit { get; set; }
		public string Category { get; set; }
		public double ProductDiscount { get; set; }
		public virtual IList<DataProductReview?>? Reviews { get; set; }
		public virtual IList<DataPurchaseOption?>? PurchaseOptions { get; set; }
	}
}
