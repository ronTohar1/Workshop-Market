using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagementObjects;

namespace MarketBackend.DataLayer.DataManagers
{
    internal class ManagerPermissionDataManager : ObjectDataManager<DataManagerPermission, int>
    {
        public ManagerPermissionDataManager(Database db) : base(db)
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


