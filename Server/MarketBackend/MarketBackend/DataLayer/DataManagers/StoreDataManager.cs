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
            elements = db.Stores; 
        }

        public virtual int GetNextId()
        {
            return this.MaxOrDefualt(elements, dataObject => dataObject.Id, 0) + 1;
        }
    }
}
