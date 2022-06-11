using MarketBackend.DataLayer.DatabaseObjects;
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
        protected BidDataManager()
        {
        }

        protected override void AddThrows(DataBid toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataBid FindThrows(int id)
        {
            DataBid? data = db.FindAsync<DataBid>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataBid> FindThrows(Predicate<DataBid> predicate)
        {
            return db.Bids.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataBid RemoveThrows(DataBid toRemove)
        {
            DataBid? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}
