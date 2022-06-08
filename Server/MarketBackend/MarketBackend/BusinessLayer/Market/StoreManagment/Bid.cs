using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class Bid
    {
        private static int idCounter = 1;
        private static Mutex counterLock = new Mutex(false);
        private static Mutex offerLock = new Mutex(false);
        public int id { get; }
        public int storeId { get; set; }
        public int productId { get; set; }
        public int memberId { get; set; }
        public double bid { get; set; }
        public IList<int> aprovingIds { get; }

        public bool counterOffer { get; set; }
        private double offer;

        private static int getId()
        {
            int temp;
            counterLock.WaitOne();
            temp = idCounter;
            idCounter++;
            counterLock.ReleaseMutex();
            return temp;
        }
        public Bid(int productId, int memberId, int storeId ,double bid)
        {
            this.id = getId();
            this.storeId = storeId;
            this.productId = productId;
            this.memberId = memberId;
            this.bid = bid;

            //counter offer
            counterOffer = false;
            offer = 0;

            aprovingIds = new SynchronizedCollection<int>();
        }

        public void approveBid(int memberId)
        {
            aprovingIds.Add(memberId);
        }

        public void CounterOffer(double offer)
        {
            offerLock.WaitOne();
            if (counterOffer) {
                offerLock.ReleaseMutex();
                throw new MarketException("a counter offer has already been made to approve!");
            }
            counterOffer = true;
            this.offer = offer;
            offerLock.ReleaseMutex();
        }

        public void approveCounterOffer()
        {
            offerLock.WaitOne();
            if (!counterOffer) { 
                offerLock.ReleaseMutex();
                throw new MarketException("No counter offer made to approve!");
            }
            counterOffer = false;
            this.bid = offer;
            offerLock.ReleaseMutex();
        }


    }
}
