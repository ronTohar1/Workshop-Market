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
        protected DiscountDataManager() : base(db => db.SimplifiedDiscounts)
        {
        }

        public virtual int GetNextId()
        {
            return this.MaxOrDefualt(elements.ToList(), dataObject => dataObject.Id, 0) + 1;
        }
    }
}

