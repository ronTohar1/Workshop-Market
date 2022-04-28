using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers
{
    interface IBuyersController
    {
        public Buyer GetBuyer(int buyerId);
    }
}
