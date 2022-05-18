using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment;

namespace MarketBackend.BusinessLayer.Admins
{
    public class AdminManager
    {
        private ICollection<int> admins;
        private StoreController storeController;
        private BuyersController buyersController;
        private MembersController membersController;
        

        public AdminManager(StoreController storeController, BuyersController buyersController, MembersController membersController)
        {
            admins = new SynchronizedCollection<int>();
            this.storeController = storeController;
            this.buyersController = buyersController;
            this.membersController = membersController;
        }

        private void VerifyAdmin(int adminId)
        {
            if (!admins.Contains(adminId) && adminId != -1)
                throw new MarketException("Permission error - user is not system admin");
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
        public bool ContainAdmin(int id)
        => admins.Contains(id);
        

        /// <summary>
        /// Removes the given admin from collection and return if deleted
        /// </summary>
        public bool RemoveAdmin(int id)
        {
            bool found = admins.Remove(id);
            while (admins.Remove(id)) ;  // due to syncornization issues, it may be that admin inserted more than once (very unlikely)
            return found;
        }

        public IReadOnlyCollection<Purchase> GetUserHistory(int adminId, int userId)
        {
            VerifyAdmin(adminId);
            Buyer? buyer = buyersController.GetBuyer(userId);
            if (buyer == null)
                throw new MarketException($"User with id={userId} does not exist");
            else
               return buyer.GetPurchaseHistory();
        }

        public IList<Purchase> GetStoreHistory(int adminId, int storeId)
        {
            VerifyAdmin(adminId);
            Store? store = storeController.GetStore(storeId);
            if (store == null)
                throw new MarketException($"store with id={storeId} does not exist");
            else
                return store.GetPurchaseHistory();
        }

        public IList<Purchase> GetStoreHistory(int adminId, string storeName) =>
            GetStoreHistory(adminId, storeController.GetStoreIdByName(storeName));

        public void RemoveMember(int adminId, int memberToRemoveId)
        {
            VerifyAdmin(adminId);
            if (storeController.HasRolesInMarket(memberToRemoveId))
                throw new MarketException($"member with id={memberToRemoveId} can't be removed since he has roles at the store");
            //from this point it's legal to ask the removal of a member from the memberController
            membersController.RemoveMember(memberToRemoveId);
        }
        public IList<int> GetLoggedInMembers(int requestingId)
        {
            VerifyAdmin(requestingId);
            return membersController.GetLoggedInMembers();
        }
        public Member? GetMemberInfo(int requestingId, int memberId)
        {
            VerifyAdmin(requestingId);
            return membersController.GetMember(memberId);
            
        }

        public bool MemberExists(int memberId)
        => membersController.GetMember(memberId) != null;
    }
}