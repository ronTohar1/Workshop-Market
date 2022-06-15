using MarketBackend.DataLayer.DataDTOs.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataManagers
{
    public class PurchaseDataManager : ObjectDataManager<DataPurchase, int>
    {
        private static PurchaseDataManager instance = null;

        public static PurchaseDataManager GetInstance()
        {
            if (instance == null)
                instance = new PurchaseDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(PurchaseDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected PurchaseDataManager()
        {
        }

        protected override void AddThrows(DataPurchase toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataPurchase FindThrows(int id)
        {
            DataPurchase? data = db.FindAsync<DataPurchase>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataPurchase> FindAll()
        {
            return db.Purchases.ToList();
        }

        protected override DataPurchase RemoveThrows(DataPurchase toRemove)
        {
            DataPurchase? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}

