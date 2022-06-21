using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;

namespace MarketBackend.DataLayer.DataManagers
{
    public class ManagerPermissionDataManager : ObjectDataManager<DataManagerPermission, int>
    {
        private static ManagerPermissionDataManager instance = null;

        public static ManagerPermissionDataManager GetInstance()
        {
            if (instance == null)
                instance = new ManagerPermissionDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(ManagerPermissionDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected ManagerPermissionDataManager() : base(db => db.ManagerPermissions, dataObject => dataObject.Id)
        {
        }
    }
}


