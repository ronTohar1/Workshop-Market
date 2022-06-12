using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Buyers.Carts
{
    public class DataProductInBag
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
