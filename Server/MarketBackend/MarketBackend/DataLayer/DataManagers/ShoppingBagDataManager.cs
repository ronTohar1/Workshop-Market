using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataManagementObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataManagers
{
    public class ShoppingBagDataManager : ObjectDataManager<DataShoppingBag, int>
    {
        private static ShoppingBagDataManager instance = null;

        public static ShoppingBagDataManager GetInstance()
        {
            if (instance == null)
                instance = new ShoppingBagDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(ShoppingBagDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected ShoppingBagDataManager()
        {
        }

        protected override void AddThrows(DataShoppingBag toAdd)
        {
            db.AddAsync(toAdd);
        }

        protected override DataShoppingBag FindThrows(int id)
        {
            DataShoppingBag? data = db.FindAsync<DataShoppingBag>(id).Result;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }

        protected override IList<DataShoppingBag> FindThrows(Predicate<DataShoppingBag> predicate)
        {
            return db.ShoppingBags.Where(entity => predicate.Invoke(entity)).ToList();
        }

        protected override DataShoppingBag RemoveThrows(DataShoppingBag toRemove)
        {
            DataShoppingBag? data = db.Remove(toRemove).Entity;
            if (data == null)
                throw new Exception("cannot be found in the database");
            return data;
        }
    }
}
