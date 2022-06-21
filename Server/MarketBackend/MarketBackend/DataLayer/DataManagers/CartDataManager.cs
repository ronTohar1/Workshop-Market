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
        protected CartDataManager() : base(db => db.Carts, dataObject => dataObject.Id)
        {
        }
    }
}