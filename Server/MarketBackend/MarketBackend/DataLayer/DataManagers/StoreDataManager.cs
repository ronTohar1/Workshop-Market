using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagementObjects;

namespace MarketBackend.DataLayer.DataManagers
{
    internal class StoreDataManager : ObjectDataManager<DataStore, int>
    {
        public StoreDataManager(Database db) : base(db)
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
