using MarketBackend.DataLayer.DataDTOs.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement
{
    public class DataBid
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public DataProduct? Product { get; set; }
        public DataMember? Member { get; set; }
        public double Bid { get; set; }
        public IList<DataMember> Approving { get; set; }
        public bool CounterOffer { get; set; }
        public double Offer { get; set; }
    }
}
