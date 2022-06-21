using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;

namespace MarketBackend.DataLayer.DataManagers
{
    public class StoreMemberRolesDataManager : ObjectDataManager<DataStoreMemberRoles, int>
    {
        private static StoreMemberRolesDataManager instance = null;

        public static StoreMemberRolesDataManager GetInstance()
        {
            if (instance == null)
                instance = new StoreMemberRolesDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(StoreMemberRolesDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected StoreMemberRolesDataManager()
        {
            elements = db.StoreMemberRoles; 
        }
    }
}


