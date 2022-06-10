using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Buyers.Carts
{
    public class DataCart
    {
        public int Id { get; set; }
        public IList<DataShoppingBag> ShoppingBags { get; set; }
    }
}
