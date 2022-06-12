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
        }

        protected override void AddThrows(DataStoreMemberRoles toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataStoreMemberRoles FindThrows(int id)
        {
            DataStoreMemberRoles? data = db.FindAsync<DataStoreMemberRoles>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataStoreMemberRoles> FindThrows(Predicate<DataStoreMemberRoles> predicate)
        {
            return db.StoreMemberRoles.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataStoreMemberRoles RemoveThrows(DataStoreMemberRoles toRemove)
        {
            DataStoreMemberRoles? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}


