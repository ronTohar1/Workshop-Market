using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataManagementObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataManagers
{
    public class ProductInBagDataManager : ObjectDataManager<DataProductInBag, int>
    {
        private static ProductInBagDataManager instance = null;

        public static ProductInBagDataManager GetInstance()
        {
            if (instance == null)
                instance = new ProductInBagDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(ProductInBagDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected ProductInBagDataManager() : base(db => db.ProductInBags, dataObject => dataObject.Id)
        {
        }
    }
}

