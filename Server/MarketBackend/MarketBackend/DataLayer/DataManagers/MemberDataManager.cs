using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagementObjects;

namespace MarketBackend.DataLayer.DataManagers
{
    public class MemberDataManager : ObjectDataManager<DataMember, int>
    {

        private static MemberDataManager instance = null;

        public static MemberDataManager GetInstance()
        {
            if (instance == null)
                instance = new MemberDataManager();
            return instance; 
        }

        public static void ForTestingSetInstance(MemberDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance; 
        }

        // protected for testing
        protected MemberDataManager()
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

