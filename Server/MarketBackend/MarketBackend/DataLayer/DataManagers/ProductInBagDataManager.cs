using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataManagementObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataManagers
{
    internal class ProductInBagDataManager : ObjectDataManager<DataProductInBag, int>
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
        protected ProductInBagDataManager()
        {
        }

        protected override void AddThrows(DataProductInBag toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataProductInBag FindThrows(int id)
        {
            DataProductInBag? data = db.FindAsync<DataProductInBag>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataProductInBag> FindThrows(Predicate<DataProductInBag> predicate)
        {
            return db.ProductInBags.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataProductInBag RemoveThrows(DataProductInBag toRemove)
        {
            DataProductInBag? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}

