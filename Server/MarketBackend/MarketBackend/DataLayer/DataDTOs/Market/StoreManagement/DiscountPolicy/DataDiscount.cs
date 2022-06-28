using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy
{
    public class DataDiscount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual DataExpression? DiscountExpression { get; set; }
    }
}
