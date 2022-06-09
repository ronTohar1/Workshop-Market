using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagementObjects;

namespace MarketBackend.DataLayer.DataManagers
{
    internal class CartDataManager : ObjectDataManager<DataCart, int>
    {
        public CartDataManager(Database db) : base(db)
        {
        }

        protected override void AddThrows(DataCart toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataCart FindThrows(int id)
        {
            DataCart? data = db.FindAsync<DataCart>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataCart> FindThrows(Predicate<DataCart> predicate)
        {
            return db.Carts.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataCart RemoveThrows(DataCart toRemove)
        {
            DataCart? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}