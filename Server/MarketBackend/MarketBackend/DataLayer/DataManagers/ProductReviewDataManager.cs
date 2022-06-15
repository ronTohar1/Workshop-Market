using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs;

namespace MarketBackend.DataLayer.DataManagers
{
    public class ProductReviewDataManager : ObjectDataManager<DataProductReview, int>
    {
        private static ProductReviewDataManager instance = null;

        public static ProductReviewDataManager GetInstance()
        {
            if (instance == null)
                instance = new ProductReviewDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(ProductReviewDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected ProductReviewDataManager()
        {
        }

        protected override void AddThrows(DataProductReview toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataProductReview FindThrows(int id)
        {
            DataProductReview? data = db.FindAsync<DataProductReview>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataProductReview> FindAll()
        {
            return db.ProductReview.ToList();
        }

        protected override DataProductReview RemoveThrows(DataProductReview toRemove)
        {
            DataProductReview? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}


