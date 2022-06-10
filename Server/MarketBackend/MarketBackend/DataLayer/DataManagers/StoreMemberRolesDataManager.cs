using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagementObjects;

namespace MarketBackend.DataLayer.DataManagers
{
    internal class StoreMemberRolesDataManager : ObjectDataManager<DataStoreMemberRoles, int>
    {
        public StoreMemberRolesDataManager(Database db) : base(db)
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


