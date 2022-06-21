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
        protected PurchaseDataManager() : base(db => db.Purchases, dataObject => dataObject.Id)
        {
        }
    }
}

