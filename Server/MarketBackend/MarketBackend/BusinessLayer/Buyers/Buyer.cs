using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using System.Collections.Concurrent;


namespace MarketBackend.BusinessLayer.Buyers
{
    public class Buyer
    {
        private static int _nextId;
        public virtual Cart Cart { get; private set; }
        public virtual int Id { get; internal set; }
        private IList<Purchase> purchaseHistory;


        private static Mutex mutex = new Mutex();

        public static int NextId
        {   
            get
            {
                lock (mutex)
                {
                    int lastId = _nextId;
                    _nextId++;
                    return lastId;
                }
            }
            private set
            {
                _nextId = value;
            }
        }

        public Buyer()
        {
            //Init properites
            Cart = new Cart();
            Id = NextId;
            purchaseHistory = new SynchronizedCollection<Purchase>();
        }

        protected Buyer(int id, Cart cart, IList<Purchase> purchaseHistory)
        {
            //Init properites
            this.Cart = cart;
            this.Id = id;
            this.purchaseHistory = purchaseHistory; 
        }


        public virtual void AddPurchase(Purchase purchase)
        {
            if (purchase == null) { throw new ArgumentNullException(nameof(purchase)); }
            this.purchaseHistory.Add(purchase);
        }

        public IReadOnlyCollection<Purchase> GetPurchaseHistory() 
        {
            return new ReadOnlyCollection<Purchase>(this.purchaseHistory);
        }

        public virtual void ChangeProductAmount(ProductInBag product, int amount, int buyerId)
        {
            int storeId = product.StoreId;
            Cart.ShoppingBags[storeId].ChangeProductAmount(product, amount, null);
        }
    }
}
