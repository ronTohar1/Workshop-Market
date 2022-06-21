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
            elements = db.ProductReview; 
        }
    }
}


