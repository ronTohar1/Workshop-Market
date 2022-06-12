using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataManagers;

namespace MarketBackend.DataLayer.DataManagementObjects
{
    public class ProductDataManager : ObjectDataManager<DataProduct, int>
    {
        private static ProductDataManager instance = null;

        public static ProductDataManager GetInstance()
        {
            if (instance == null)
                instance = new ProductDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(ProductDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected ProductDataManager()
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
