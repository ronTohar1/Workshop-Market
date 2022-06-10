using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataManagementObjects;

namespace MarketBackend.DataLayer.DataManagers
{
    internal class ProductReviewDataManager : ObjectDataManager<DataProductReview, int>
    {
        public ProductReviewDataManager(Database db) : base(db)
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

        protected override IList<DataProductReview> FindThrows(Predicate<DataProductReview> predicate)
        {
            return db.ProductReview.Where(entity => predicate.Invoke(entity)).ToList();
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


