using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs;


namespace MarketBackend.DataLayer.DataManagementObjects
{
    public class ProductDataManager : ObjectDataManager<DataProduct, int>
    {
        public ProductDataManager(Database db) : base(db)
        {
        }

        protected override void AddThrows(DataProduct toAdd)
        {
            db.AddAsync(toAdd); 
        }

        protected override DataProduct FindThrows(int id)
        {
            DataProduct? dp = db.FindAsync<DataProduct>(id).Result;
            if (dp == null)
                throw new Exception("cannot be found in the database");
            return dp;
        }

        protected override IList<DataProduct> FindThrows(Predicate<DataProduct> predicate)
        {
            return db.Products.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataProduct RemoveThrows(DataProduct toRemove)
        {
            DataProduct? dp = db.Remove(toRemove).Entity;
            if (dp == null)
                throw new Exception("cannot be found in the database");
            return dp;
        }

    }
}
