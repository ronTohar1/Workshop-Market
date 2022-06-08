using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagementObjects;


namespace MarketBackend.DataLayer.DataManagers
{
    public class BidDataManager : ObjectDataManager<DataBid, int>
    {
        public BidDataManager(Database db) : base(db)
        {
        }

        protected override void AddThrows(DataBid toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataBid FindThrows(int id)
        {
            DataBid? data = db.FindAsync<DataBid>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataBid> FindThrows(Predicate<DataBid> predicate)
        {
            return db.Bids.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataBid RemoveThrows(DataBid toRemove)
        {
            DataBid? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}
