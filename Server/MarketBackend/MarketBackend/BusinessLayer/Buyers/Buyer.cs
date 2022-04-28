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
                mutex.WaitOne();
                int lastId = _nextId;
                NextId++;
                mutex.ReleaseMutex();
                return lastId;
            }
            private set
            {
                _nextId = value;
            }
        }

        public Buyer()
        {
            Cart = new Cart();
            Id = NextId;
            purchaseHistory = new SynchronizedCollection<Purchase>();
        }


    }
}
