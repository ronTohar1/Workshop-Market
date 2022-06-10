using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagementObjects;

namespace MarketBackend.DataLayer.DataManagers
{
    internal class MemberDataManager : ObjectDataManager<DataMember, int>
    {
        public MemberDataManager(Database db) : base(db)
        {
        }

        protected override void AddThrows(DataMember toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataMember FindThrows(int id)
        {
            DataMember? data = db.FindAsync<DataMember>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataMember> FindThrows(Predicate<DataMember> predicate)
        {
            return db.Members.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataMember RemoveThrows(DataMember toRemove)
        {
            DataMember? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

    }
}

