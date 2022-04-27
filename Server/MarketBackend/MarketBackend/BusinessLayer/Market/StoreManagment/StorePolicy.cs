using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class StorePolicy
    {
        private List<PurchaseOption> purchaseOptions { get;  }
        private Dictionary<Product, int> minAmountPerProduct { get;  }

    }
}
