using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class StorePolicy
    {
        private IList<PurchaseOption> purchaseOptions { get;  }
        private IDictionary<int, int> minAmountPerProduct { get;  }

        private Mutex purchaseOptionsMutex;
        private Mutex minAmountPerProductMutex;
        public StorePolicy() {
            purchaseOptions = new SynchronizedCollection<PurchaseOption>();
            minAmountPerProduct = new ConcurrentDictionary<int, int>();

            purchaseOptionsMutex = new Mutex();
            minAmountPerProductMutex = new Mutex();
        }
        public void AddPurchaseOption(PurchaseOption purchaseOption)
        {
            purchaseOptionsMutex.WaitOne();
            if (!purchaseOptions.Contains(purchaseOption))
                purchaseOptions.Add(purchaseOption);
            purchaseOptionsMutex.ReleaseMutex();
        }
        public void RemovePurchaseOption(PurchaseOption purchaseOption)
        {
            purchaseOptionsMutex.WaitOne();
            if (!purchaseOptions.Contains(purchaseOption))
            {
                purchaseOptionsMutex.ReleaseMutex();
                throw new MarketException("there isn't such purchase option at the store");
            }
            purchaseOptions.Remove(purchaseOption);
            purchaseOptionsMutex.ReleaseMutex();
        }
        public void SetMinAmountPerProduct(int productId, int amount)
        {
            minAmountPerProductMutex.WaitOne();
            minAmountPerProduct[productId] = amount;
            minAmountPerProductMutex.ReleaseMutex();
        }

        public bool ContainsPurchaseOption(PurchaseOption purchaseOption) 
        => purchaseOptions.Contains(purchaseOption);
        
        public int GetMinAmountPerProduct(int productId) // for each product at the store that isn't in this class the default value is 0 
        => minAmountPerProduct.ContainsKey(productId) ? minAmountPerProduct[productId] : 0;

    }
}
