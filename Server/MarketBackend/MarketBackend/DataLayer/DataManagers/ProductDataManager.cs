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
        protected ProductDataManager() : base(db => db.Products, dataObject => dataObject.Id)
        {
        }

        public virtual int GetNextId()
        {
            return this.MaxOrDefualt(db.Products, dataObject => dataObject.Id, 0) + 1;
        }
    }
}
