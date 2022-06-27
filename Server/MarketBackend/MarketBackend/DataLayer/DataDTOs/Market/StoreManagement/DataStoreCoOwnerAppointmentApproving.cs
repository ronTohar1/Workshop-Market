using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement
{
    public class DataStoreCoOwnerAppointmentApproving
    {
        public virtual int Id { get; set; }
        public virtual int newCoOwnerId { get; set; }
        public virtual int ApprovingMemberId { get; set; }
        public virtual bool AppointedFirst { get; set; }
    }
}
