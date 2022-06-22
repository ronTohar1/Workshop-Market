using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;

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
        protected MemberDataManager() : base(db => db.SimplifiedMembers)
        {
        }

        public virtual int GetNextId()
        {
            return this.MaxOrDefualt(elements.ToList(), member => member.Id, 0) + 1; 
        }
    }
}

