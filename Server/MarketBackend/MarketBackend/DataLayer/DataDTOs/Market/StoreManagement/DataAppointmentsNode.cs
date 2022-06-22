using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement
{
    public class DataAppointmentsNode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [ForeignKey("DataMember")]
        public int MemberId { get; set; }
        public IList<DataAppointmentsNode> Children { get; set; }
    }
}
