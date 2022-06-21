using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DatabaseObjects.DbSetMocks;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;


namespace MarketBackend.DataLayer.DataManagers
{
    public class BidDataManager : ObjectDataManager<DataBid, int>
    {
        private static BidDataManager instance = null;

        public static BidDataManager GetInstance()
        {
            if (instance == null)
                instance = new BidDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(BidDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected BidDataManager() : base(db => db.Bids, dataObject => dataObject.Id)
        {
            
        }

        public virtual int GetNextId()
        {
            return this.MaxOrDefualt(db.Members, member => member.Id, 0) + 1;
        }
    }
}
