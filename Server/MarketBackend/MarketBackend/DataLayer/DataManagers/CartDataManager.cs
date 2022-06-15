using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;

namespace MarketBackend.DataLayer.DataManagers
{
    public class CartDataManager : ObjectDataManager<DataCart, int>
    {
        private static CartDataManager instance = null;

        public static CartDataManager GetInstance()
        {
            if (instance == null)
                instance = new CartDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(CartDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected CartDataManager()
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

        protected override IList<DataCart> FindAll()
        {
            return db.Carts.ToList();
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