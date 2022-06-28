using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataManagers
{
    public class DailyMarketStatisticsDataManager : ObjectDataManager<DataDailyMarketStatistics, int>
    {
        private static DailyMarketStatisticsDataManager instance = null;

        private static Mutex statisticsMutex = new Mutex(); // using mutex for efficiency 

        public static DailyMarketStatisticsDataManager GetInstance()
        {
            if (instance == null)
                instance = new DailyMarketStatisticsDataManager();
            return instance;
        }

        public static void ForTestingSetInstance(DailyMarketStatisticsDataManager argumentInstance)
        {
            if (argumentInstance == null)
                throw new ArgumentException("this function is for testing, and needs to get a not null instance");
            instance = argumentInstance;
        }

        // protected for testing
        protected DailyMarketStatisticsDataManager() : base(db => db.SimplifiedDailyMarketStatistics)
        {

        }

        public DataDailyMarketStatistics GetCurrentDailyMarketStatistics()
        {
            statisticsMutex.WaitOne();
            try
            {
                IList<DataDailyMarketStatistics> dailyMarketStatisticsElements = this.FindAll();
                DateTime lastDate = dailyMarketStatisticsElements.Max(dailyMarketStatistics => dailyMarketStatistics.date);

                if (lastDate.Date == DateTime.Now.Date)
                    return dailyMarketStatisticsElements.FirstOrDefault(dailyMarketStatistics => dailyMarketStatistics.date == lastDate);

                int newDailyMarketStatisticsId = AddDailyMarektStatistics();
                return this.Find(newDailyMarketStatisticsId);
            }
            finally
            {
                statisticsMutex.ReleaseMutex();
            }
        }

        private int AddDailyMarektStatistics()
        {
            DataDailyMarketStatistics toAdd = new DataDailyMarketStatistics()
            {
                date = DateTime.Now,
                NumberOfAdminsLogin = 0,
                NumberOfCoOwnersLogin = 0,
                NumberOfManagersLogin = 0,
                NumberOfMembersLogin = 0,
                NumberOfGuestsLogin = 0
            };

            this.Add(toAdd);
            this.Save();

            return toAdd.Id; 
        }

        //public void AddAdminLogin()
        //{
        //    UpdateCurrentDailyMarketStatistics(dailyMarketStatistics =>
        //    {
        //        dailyMarketStatistics.NumberOfAdminsLogin += 1;
        //    });
        //}

        //public void AddCoOwnerLogin()
        //{
        //    UpdateCurrentDailyMarketStatistics(dailyMarketStatistics =>
        //    {
        //        dailyMarketStatistics.NumberOfCoOwnersLogin += 1;
        //    });
        //}

        //public void AddManagerLogin()
        //{
        //    UpdateCurrentDailyMarketStatistics(dailyMarketStatistics =>
        //    {
        //        dailyMarketStatistics.NumberOfManagersLogin += 1;
        //    });
        //}

        //public void AddMemberLogin()
        //{
        //    UpdateCurrentDailyMarketStatistics(dailyMarketStatistics =>
        //    {
        //        dailyMarketStatistics.NumberOfMembersLogin += 1;
        //    });
        //}

        //public void AddGuestLogin()
        //{
        //    UpdateCurrentDailyMarketStatistics(dailyMarketStatistics =>
        //    {
        //        dailyMarketStatistics.NumberOfGuestsLogin += 1;
        //    });
        //}

        //public void UpdateCurrentDailyMarketStatistics(Action<DataDailyMarketStatistics> action)
        //{
        //    DataDailyMarketStatistics dailyMarketStatistics = GetCurrentDailyMarketStatistics();

        //    statisticsMutex.WaitOne();
        //    try
        //    {
        //        action(dailyMarketStatistics);
        //    }
        //    finally
        //    {
        //        statisticsMutex.ReleaseMutex();
        //    }
        //}

        //public IList<DataDailyMarketStatistics> GetBetweenDates(DateOnly from, DateOnly to)
        //{
        //    return this.Find(dailyMarketStatistics => 
        //        from.CompareTo(dailyMarketStatistics.date) <= 0 && dailyMarketStatistics.date.CompareTo(to) <= 0);
        //}
    }
}
