using System;
using System.Collections.Generic;
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
        public Cart Cart { get; internal set; }
        public int Id { get; internal set; }
        private IList<Purchase> purchaseHistory;


        private static Mutex mutex = new Mutex();

        public static int NextId
        {   
            get
            {
                lock (mutex)
                {
                    int lastId = _nextId;
                    NextId++;
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


        public bool AddPurchase(Purchase purchase)
        {
            throw new NotImplementedException();
        }

    }
}
