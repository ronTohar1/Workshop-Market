using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataManagers
{
    public class HierarchyDataManager : ObjectDataManager<DataAppointmentsNode, int>
    {
        private static HierarchyDataManager instance = null;

        public static HierarchyDataManager GetInstance()
        {
            if (instance == null)
                instance = new HierarchyDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(HierarchyDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected HierarchyDataManager() : base(db => db.SimplifiedAppointmentsNodes)
        {
        }

        public virtual int GetNextId()
        {
            return this.MaxOrDefualt(elements.ToList(), dataObject => dataObject.Id, 0) + 1;
        }
    }
}
