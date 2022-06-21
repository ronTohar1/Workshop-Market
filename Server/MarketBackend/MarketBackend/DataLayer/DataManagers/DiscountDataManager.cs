using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;

namespace MarketBackend.DataLayer.DataManagers
{
    public class DiscountDataManager : ObjectDataManager<DataDiscount, int>
    {
        private static DiscountDataManager instance = null;

        public static DiscountDataManager GetInstance()
        {
            if (instance == null)
                instance = new DiscountDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(DiscountDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected DiscountDataManager()
        {
            elements = db.Discounts; 
        }

        public virtual int GetNextId()
        {
            return this.MaxOrDefualt(db.Discounts, dataObject => dataObject.Id, 0) + 1;
        }
    }
}

