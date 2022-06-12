using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;

namespace MarketBackend.DataLayer.DataManagers
{
    public class StoreDataManager : ObjectDataManager<DataStore, int>
    {
        private static StoreDataManager instance = null;

        public static StoreDataManager GetInstance()
        {
            if (instance == null)
                instance = new StoreDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(StoreDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected StoreDataManager()
        {
        }

        protected override void AddThrows(DataStore toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataStore FindThrows(int id)
        {
            DataStore? data = db.FindAsync<DataStore>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataStore> FindThrows(Predicate<DataStore> predicate)
        {
            return db.Stores.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataStore RemoveThrows(DataStore toRemove)
        {
            DataStore? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}
