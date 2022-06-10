using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagementObjects;

namespace MarketBackend.DataLayer.DataManagers
{
    public class PurchaseOptionsDataManager : ObjectDataManager<DataPurchaseOption, int>
    {
        private static PurchaseOptionsDataManager instance = null;

        public static PurchaseOptionsDataManager GetInstance()
        {
            if (instance == null)
                instance = new PurchaseOptionsDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(PurchaseOptionsDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected PurchaseOptionsDataManager()
        {
        }

        protected override void AddThrows(DataPurchaseOption toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataPurchaseOption FindThrows(int id)
        {
            DataPurchaseOption? data = db.FindAsync<DataPurchaseOption>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataPurchaseOption> FindThrows(Predicate<DataPurchaseOption> predicate)
        {
            return db.PurchaseOptions.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataPurchaseOption RemoveThrows(DataPurchaseOption toRemove)
        {
            DataPurchaseOption? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}


