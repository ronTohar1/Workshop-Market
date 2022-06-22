using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;

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
        protected PurchaseOptionsDataManager() : base(db => db.SimplifiedPurchaseOptions)
        {
        }
    }
}


