using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagementObjects;
using MarketBackend.DataLayer.DataManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class Bid
    {
        private const int ID_COUNTER_NOT_INITIALIZED = -1;
        private static int idCounter = ID_COUNTER_NOT_INITIALIZED; 
        private static Mutex counterLock = new Mutex(false);
        private static Mutex offerLock = new Mutex(false);
        public int id { get; }
        public int storeId { get; set; }
        public int productId { get; set; }
        public int memberId { get; set; }
        public double bid { get; private set; }
        public IList<int> aprovingIds { get; }

        public bool counterOffer { get; private set; }
        private double offer;

        private BidDataManager bidDataManager;

        private static int getId()
        {
            int temp;
            counterLock.WaitOne();

            if (idCounter == ID_COUNTER_NOT_INITIALIZED)
                InitializeIdCounter(); 

            temp = idCounter;
            idCounter++;
            counterLock.ReleaseMutex();
            return temp;
        }

        private static void InitializeIdCounter()
        {
            idCounter = BidDataManager.GetInstance().GetNextId();
        }

        public Bid(int productId, int memberId, int storeId ,double bid) 
            : this(
                  getId(), storeId, productId, memberId, bid,
                  new SynchronizedCollection<int>(), 
                  false, 0 //counter offer
                  )
        {

        }

        private Bid(int id, int storeId, int productId, int memberId, double bid,
            IList<int> aprovingIds, bool counterOffer, double offer)
        {
            if (storeId < 0 || productId < 0 || memberId < 0)
                throw new Exception("Illegal id given");

            if (bid < 0)
                throw new MarketException("bid cannot be negative");

            this.id = id;
            this.storeId = storeId;
            this.productId = productId;
            this.memberId = memberId;
            this.bid = bid;

            //counter offer
            this.counterOffer = counterOffer;
            this.offer = offer;

            this.aprovingIds = aprovingIds;

            this.bidDataManager = BidDataManager.GetInstance();
        }

        // r S 8
        public static Bid DataBidToBid(DataBid dataBid, int storeId)
        {
            IList<int> aprovingIds = new SynchronizedCollection<int>(); 

            foreach(int approingId in dataBid.Approving.Select(dataMember => dataMember.Id))
            {
                aprovingIds.Add(approingId); 
            }

            return new Bid(dataBid.Id, storeId, dataBid.ProdctId, dataBid.MemberId,
                dataBid.Bid, aprovingIds, dataBid.CounterOffer, dataBid.Offer); 
        }

        public DataBid ToNewDataBid()
        {
            return new DataBid()
            {
                Id = this.id,
                ProdctId = productId,
                MemberId = memberId,
                Bid = bid,
                Approving = aprovingIds.Select(approvingId => new DataBidMemberId() { MemberId = approvingId }).ToList(),
                CounterOffer = counterOffer,
                Offer = offer
            };
        }
        public void approveBid(int memberId, Action saveChanges)
        {
            bidDataManager.Update(id, dataBid => dataBid.Approving.Add(new DataBidMemberId { MemberId = memberId}));

            saveChanges(); 

            aprovingIds.Add(memberId);
        }

        public void CounterOffer(double offer)
        {
            offerLock.WaitOne();
            if (counterOffer) {
                offerLock.ReleaseMutex();
                throw new MarketException("a counter offer has already been made to approve!");
            }
            bidDataManager.Update(id, dataBid =>
            {
                dataBid.CounterOffer = true;
                dataBid.Offer = offer;
            });
            bidDataManager.Save(); 

            counterOffer = true;
            this.offer = offer;
            offerLock.ReleaseMutex();
        }

        public void approveCounterOffer(Action saveChanges)
        {
            offerLock.WaitOne();
            if (!counterOffer) { 
                offerLock.ReleaseMutex();
                throw new MarketException("No counter offer made to approve!");
            }

            bidDataManager.Update(id, dataBid =>
            {
                dataBid.CounterOffer = false;
                dataBid.Bid = offer;
            });
            saveChanges();

            counterOffer = false;
            this.bid = offer;
            offerLock.ReleaseMutex();
        }

        public void DataRemove()
        {
            bidDataManager.Remove(id); 
        }
    }
}
