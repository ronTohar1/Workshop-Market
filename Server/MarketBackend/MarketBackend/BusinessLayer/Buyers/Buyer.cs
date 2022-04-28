using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers
{
    public class Buyer
    {
        public Cart Cart { get; internal set; }

        public Buyer()
        {
            Cart = new Cart();
        }
    }
}
