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
using MarketBackend.DataLayer.DataManagers;
using MarketBackend.DataLayer.DataDTOs.Buyers;

namespace MarketBackend.BusinessLayer.Admins
{
    public class AdminManager
    {
        private ICollection<int> admins;
        private StoreController storeController;
        private BuyersController buyersController;
        private MembersController membersController;
        

        public AdminManager(StoreController storeController, BuyersController buyersController, MembersController membersController) 
            : this(storeController, buyersController, membersController, new SynchronizedCollection<int>())
        {

        }

        private AdminManager(StoreController storeController, BuyersController buyersController, MembersController membersController, ICollection<int> admins)
        {
            this.admins = admins;
            this.storeController = storeController;
            this.buyersController = buyersController;
            this.membersController = membersController;
        }

        // r S 8
        public static AdminManager LoadAdminManager(StoreController storeController, BuyersController buyersController, MembersController membersController)
        {
            MemberDataManager memberDataManager = MemberDataManager.GetInstance();

            IList<DataMember> dataAdmins = memberDataManager.Find(dataMember => dataMember.IsAdmin);

            ICollection<int> adminsIds = new SynchronizedCollection<int>();
            foreach (DataMember dataAdmin in dataAdmins)
            {
                adminsIds.Add(dataAdmin.Id);
            }

            return new AdminManager(storeController, buyersController, membersController, adminsIds); 
        }

        // to use before uplodaing all the system 
        public static bool ContainsAdmin(int adminId)
        {
            DataMember dataMember = MemberDataManager.GetInstance().Find(adminId);
            return dataMember != null && dataMember.IsAdmin;
        }

        private void VerifyAdmin(int adminId)
        {
            if (!admins.Contains(adminId))
                throw new MarketException("Permission error - user is not system admin");
        }

        /// <summary>
        /// Adds new system admin and returns if admin has added (false if not a legal action)
        /// </summary>
        public bool AddAdmin(int id)
        {
            if (admins.Contains(id))
                return false;

            Member member = membersController.GetMember(id);
            if (member == null)
                return false;

            MemberDataManager memberDataManager = MemberDataManager.GetInstance(); 
            memberDataManager.Update(id, member => member.IsAdmin = true);
            memberDataManager.Save(); 

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
            if (!ContainAdmin(id))
                return false; 

            MemberDataManager memberDataManager = MemberDataManager.GetInstance();
            memberDataManager.Update(id, member => member.IsAdmin = false);
            memberDataManager.Save();

            while (admins.Remove(id)) ;  // due to syncornization issues, it may be that admin inserted more than once (very unlikely)
            return true;
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

        public bool RemoveMember(int adminId, int memberToRemoveId)
        {
            VerifyAdmin(adminId);
            if (storeController.HasRolesInMarket(memberToRemoveId))
                throw new MarketException("Sorry dear admin, but this user has a role in a store");
            //from this point it's legal to ask the removal of a member from the memberController
            membersController.RemoveMember(memberToRemoveId);
            return true;
        }
        public IDictionary<int, Member> GetLoggedInMembers(int requestingId)
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

        public double GetSystemDailyProfit(int requestingId)
        {
            VerifyAdmin(requestingId);
            double total = 0;
            foreach (Store store in storeController.GetOpenStores().Values)
                total += store.GetDailyProfit();
            return total;
        }
    }
}