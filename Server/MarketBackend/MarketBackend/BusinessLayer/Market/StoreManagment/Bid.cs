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
        public int id { get; }
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
        public Bid(int productId, int memberId, double bid)
        {
            this.id = getId();
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
            counterOffer = true;
            this.offer = offer;
        }

        public void approveCounterOffer()
        {
            if (!counterOffer)
                throw new MarketException("No counter offer made to approve!");
            counterOffer = false;
            this.bid = offer;
        }


    }
}
