using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement
{
    public class DataDailyMarketStatistics
    {
        public virtual int Id { get; set; }
        public virtual DateTime date { get; set; }

        public virtual int NumberOfAdminsLogin { get; set; }
        public virtual int NumberOfCoOwnersLogin { get; set; }
        public virtual int NumberOfManagersLogin { get; set; }
        public virtual int NumberOfMembersLogin { get; set; }
        public virtual int NumberOfGuestsLogin { get; set; }
    }
}
