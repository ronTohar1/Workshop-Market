using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    public class ServiceDateRestriction : ServiceRestriction
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }

        //cant buy on that date
        public ServiceDateRestriction(int year = -1, int month = -1, int day = -1, string tag = "") : base(tag)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }
    }
}
