using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Buyers
{
    public class DataNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Notification { get; set; }
    }
}
