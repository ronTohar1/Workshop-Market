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
        }

        protected override void AddThrows(DataDiscount toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataDiscount FindThrows(int id)
        {
            DataDiscount? data = db.FindAsync<DataDiscount>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataDiscount> FindAll()
        {
            return db.Discounts.ToList();
        }

        protected override DataDiscount RemoveThrows(DataDiscount toRemove)
        {
            DataDiscount? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        public virtual int GetNextId()
        {
            return this.MaxOrDefualt(db.Discounts, dataObject => dataObject.Id, 0) + 1;
        }
    }
}

