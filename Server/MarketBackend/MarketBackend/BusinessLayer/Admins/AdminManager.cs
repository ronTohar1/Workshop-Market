using System;
using System.IO;
using System.Windows;
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
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using SystemLog;

namespace MarketBackend.BusinessLayer.Admins
{
    public class AdminManager
    {
        private ICollection<int> admins;
        private StoreController storeController;
        private BuyersController buyersController;
        private MembersController membersController;

        private IDictionary<int, DataDailyMarketStatistics> marketStatistics; // id to daily market statistics 
        private ReaderWriterLock marketStatisticsLock;
        private const int timeoutMilis = 3000; // time for wating for the rw lock, after which it throws an exception


        private DailyMarketStatisticsDataManager dailyMarketStatisticsDataManager;

        public AdminManager(StoreController storeController, BuyersController buyersController, MembersController membersController) 
            : this(storeController, buyersController, membersController, new SynchronizedCollection<int>(), new ConcurrentDictionary<int, DataDailyMarketStatistics>())
        {

        }

        private AdminManager(StoreController storeController, BuyersController buyersController, MembersController membersController, ICollection<int> admins, IDictionary<int, DataDailyMarketStatistics> marketStatistics)
        {
            this.admins = admins;
            this.storeController = storeController;
            this.buyersController = buyersController;
            this.membersController = membersController;

            this.marketStatistics = marketStatistics;

            dailyMarketStatisticsDataManager = DailyMarketStatisticsDataManager.GetInstance();

            marketStatisticsLock = new ReaderWriterLock();
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

            IList<DataDailyMarketStatistics> dataMarketStatistics = DailyMarketStatisticsDataManager.GetInstance().Find(dataDailyMarketStatistics => true);
            IDictionary<int, DataDailyMarketStatistics> marketStatistics = new ConcurrentDictionary<int, DataDailyMarketStatistics>();
            foreach (DataDailyMarketStatistics dailyMarketStatistics in dataMarketStatistics)
            {
                marketStatistics.Add(dailyMarketStatistics.Id, dailyMarketStatistics);
            }

            return new AdminManager(storeController, buyersController, membersController, adminsIds, marketStatistics); 
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

        internal string GetEventLogs(int userId)
        {
            VerifyAdmin(userId);
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\MarketBackend\", @"SystemLog\event_logs.txt");
            return File.ReadAllText(path);

        }

        internal string GetErrorLogs(int userId)
        {
            VerifyAdmin(userId);
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\MarketBackend\", @"SystemLog\error_logs.txt");
            return File.ReadAllText(path);
        }

        public void OnMemberLogin(int memberId)
        {
            UppdateDailyMarketStatistics(dailyMarketStatistics =>
            {
                if (admins.Contains(memberId))
                    dailyMarketStatistics.NumberOfAdminsLogin += 1;
                else if (HasRoleInMarket(memberId, Role.Owner))
                    dailyMarketStatistics.NumberOfCoOwnersLogin += 1;
                else if (HasRoleInMarket(memberId, Role.Manager))
                    dailyMarketStatistics.NumberOfManagersLogin += 1;
                else
                    dailyMarketStatistics.NumberOfMembersLogin += 1;
            }, "On member login, id: " + memberId);
        }

        public void OnGuestEnter()
        {
            UppdateDailyMarketStatistics(dailyMarketStatistics =>
            {
                dailyMarketStatistics.NumberOfGuestsLogin += 1;
            }, "On guest enter");
        }

        private void UppdateDailyMarketStatistics(Action<DataDailyMarketStatistics> action, string updateDescription)
        {
            marketStatisticsLock.AcquireWriterLock(timeoutMilis);
            try
            {
                DataDailyMarketStatistics currentDailyMarketStatistics = dailyMarketStatisticsDataManager.GetCurrentDailyMarketStatistics();

                action(currentDailyMarketStatistics);

                dailyMarketStatisticsDataManager.Save();

                if (marketStatistics.ContainsKey(currentDailyMarketStatistics.Id))
                    marketStatistics[currentDailyMarketStatistics.Id] = currentDailyMarketStatistics;
                else
                    marketStatistics.Add(currentDailyMarketStatistics.Id, currentDailyMarketStatistics);
            }
            catch (Exception exception)
            {
                SystemLogger.getLogger().Error(exception, $"Business layer, on updating daily statistics: " + updateDescription);
            }
            finally
            {
                marketStatisticsLock.ReleaseWriterLock();
            }
        }

        private bool HasRoleInMarket(int memberId, Role role)
        {
            ProductsSearchFilter filter = new ProductsSearchFilter();
            filter.FilterStoreOfMemberInRole(memberId, role);
            return storeController.SearchOpenStores(filter).Count > 0; 
        }

        private IList<DataDailyMarketStatistics> GetDataDailyMarketStatisticsBetweenDates(DateOnly from, DateOnly to)
        {
            marketStatisticsLock.AcquireReaderLock(timeoutMilis);
            try 
            {
                return marketStatistics.Values.Where(dailyMarketStatistics =>
                    from.CompareTo(dailyMarketStatistics.date) <= 0 && dailyMarketStatistics.date.CompareTo(to) <= 0).ToList();
            }
            finally 
            {
                marketStatisticsLock.ReleaseReaderLock();
            }
        }

        // [number_of_admin_visits, number_of_storeOwners_visitors, number_of_managers_without_any_stores_visits,
        // number_of_simple_members(not manager or store owner), number_of_guests]
        public int[] GetMarketStatisticsBetweenDates(int adminId, DateOnly from, DateOnly to)
        {
            VerifyAdmin(adminId);

            if (to < from)
                throw new MarketException("the second date needs to be at least the first date, not before it");

            int[] loginsCounts = new int[5];

            IList<DataDailyMarketStatistics> dailyMarketStatisticsBtweenDates = GetDataDailyMarketStatisticsBetweenDates(from, to);
            foreach (DataDailyMarketStatistics dailyMarketStatistics in dailyMarketStatisticsBtweenDates)
            {
                loginsCounts[0] += dailyMarketStatistics.NumberOfAdminsLogin;
                loginsCounts[1] += dailyMarketStatistics.NumberOfCoOwnersLogin;
                loginsCounts[2] += dailyMarketStatistics.NumberOfManagersLogin;
                loginsCounts[3] += dailyMarketStatistics.NumberOfMembersLogin;
                loginsCounts[4] += dailyMarketStatistics.NumberOfGuestsLogin;
            }

            return loginsCounts;
        }
    }
}