using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class StorePolicy
    {
        private IList<PurchaseOption> purchaseOptions { get;  }
        private IDictionary<Product, int> minAmountPerProduct { get;  }



    }
}
