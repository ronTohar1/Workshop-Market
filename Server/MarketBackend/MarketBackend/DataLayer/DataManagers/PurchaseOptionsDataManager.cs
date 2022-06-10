using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagementObjects;

namespace MarketBackend.DataLayer.DataManagers
{
    internal class PurchaseOptionsDataManager : ObjectDataManager<DataPurchaseOption, int>
    {
        public PurchaseOptionsDataManager(Database db) : base(db)
        {
        }

        protected override void AddThrows(DataPurchaseOption toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataPurchaseOption FindThrows(int id)
        {
            DataPurchaseOption? data = db.FindAsync<DataPurchaseOption>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataPurchaseOption> FindThrows(Predicate<DataPurchaseOption> predicate)
        {
            return db.PurchaseOptions.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataPurchaseOption RemoveThrows(DataPurchaseOption toRemove)
        {
            DataPurchaseOption? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}


