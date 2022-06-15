using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataManagers
{
    public class PurchasePolicyDataManager : ObjectDataManager<DataPurchasePolicy, int>
    {
        private static PurchasePolicyDataManager instance = null;

        public static PurchasePolicyDataManager GetInstance()
        {
            if (instance == null)
                instance = new PurchasePolicyDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(PurchasePolicyDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected PurchasePolicyDataManager()
        {
        }

        protected override void AddThrows(DataPurchasePolicy toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataPurchasePolicy FindThrows(int id)
        {
            DataPurchasePolicy? data = db.FindAsync<DataPurchasePolicy>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataPurchasePolicy> FindAll()
        {
            return db.PurchasePolicies.ToList();
        }

        protected override DataPurchasePolicy RemoveThrows(DataPurchasePolicy toRemove)
        {
            DataPurchasePolicy? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        public virtual int GetNextId()
        {
            return this.MaxOrDefualt(db.PurchasePolicies, dataObject => dataObject.Id, 0) + 1;
        }
    }
}

