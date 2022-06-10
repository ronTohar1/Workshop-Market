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
        protected HierarchyDataManager()
        {
        }

        protected override void AddThrows(DataAppointmentsNode toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataAppointmentsNode FindThrows(int id)
        {
            DataAppointmentsNode? data = db.FindAsync<DataAppointmentsNode>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataAppointmentsNode> FindThrows(Predicate<DataAppointmentsNode> predicate)
        {
            return db.AppointmentsNodes.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataAppointmentsNode RemoveThrows(DataAppointmentsNode toRemove)
        {
            DataAppointmentsNode? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}
