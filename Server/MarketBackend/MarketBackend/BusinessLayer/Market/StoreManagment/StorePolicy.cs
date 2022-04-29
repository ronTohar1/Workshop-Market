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
        public  IDictionary<int, int> minAmountPerProduct { get; private set; }
        public SortedDictionary<int, double> amountDiscount { get; private set; } // note that this may be a not thread safe Collection, but for that we use the amountDiscountMutex

        private Mutex purchaseOptionsMutex;
        private Mutex minAmountPerProductMutex;
        private Mutex amountDiscountMutex;
        public StorePolicy() {
            purchaseOptions = new SynchronizedCollection<PurchaseOption>();
            minAmountPerProduct = new ConcurrentDictionary<int, int>();
            amountDiscount = new SortedDictionary<int, double>();

            purchaseOptionsMutex = new Mutex();
            minAmountPerProductMutex = new Mutex();
            amountDiscountMutex = new Mutex();
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

        public void AddDiscountAmountPolicy(int amount, double discountPercentage)
        {
            amountDiscountMutex.WaitOne();
            amountDiscount.Add(amount, discountPercentage/100);
            amountDiscountMutex.ReleaseMutex();
        }
        public double GetDiscountForAmount(int amount)
        {
            amountDiscountMutex.WaitOne();
            if (amountDiscount.Count == 0)
                return 0.0;
            int biggestClosestAmount = amountDiscount.Keys.First();
            foreach (int key in amountDiscount.Keys) {
                if (amountDiscount[key] < amount)
                    biggestClosestAmount = key;
                else
                    break;
            }
            amountDiscountMutex.ReleaseMutex();
            return amountDiscount[biggestClosestAmount];
        }
    }
}
