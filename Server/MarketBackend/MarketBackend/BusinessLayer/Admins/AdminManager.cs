using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment;

namespace MarketBackend.BusinessLayer.Admins
{
    internal class AdminManager
    {
        private ICollection<int> admins;
        private StoreController storeController;
        private BuyersController buyersController;
        

        public AdminManager(StoreController storeController, BuyersController buyersController)
        {
            admins = new SynchronizedCollection<int>();
            this.storeController = storeController;
            this.buyersController = buyersController;
        }

        /// <summary>
        /// Adds new system admin and returns if admin has added (false if already existed)
        /// </summary>
        public bool AddAdmin(int id)
        {
            if (admins.Contains(id))
                return false;

            admins.Add(id);
            return true;
        }

        /// <summary>
        /// Removes the given admin from collection and return if deleted
        /// </summary>
        public bool RemoveAdmin(int id)
        {
            bool found = admins.Remove(id);
            while (admins.Remove(id)) ;  // due to syncornization issues, it may be that admin inserted more than once (very unlikely)
            return found;
        }

        public IList<Purchase> GetUserHistory(int adminId, int userId)
        {
            return new List<Purchase>();
            //return buyersController.GetBuyer(userId).GetPurchaseHistory();
        }

        public IList<Purchase> GetStoreHistory(int adminId, int storeId)
        {
            return new List<Purchase>();
            //return storeController.getStore(storeId).GetPurchaseHistory();
        }

        public IList<Purchase> GetStoreHistory(int adminId, string storeName) =>
            GetStoreHistory(adminId, storeController.GetStoreIdByName(storeName));
    }
}
