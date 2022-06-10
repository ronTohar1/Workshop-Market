using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagementObjects;

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
        protected ManagerPermissionDataManager()
        {
        }

        protected override void AddThrows(DataManagerPermission toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataManagerPermission FindThrows(int id)
        {
            DataManagerPermission? data = db.FindAsync<DataManagerPermission>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataManagerPermission> FindThrows(Predicate<DataManagerPermission> predicate)
        {
            return db.ManagerPermissions.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataManagerPermission RemoveThrows(DataManagerPermission toRemove)
        {
            DataManagerPermission? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}


