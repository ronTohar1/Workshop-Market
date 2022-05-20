using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    internal class ServiceDateRestriction : IServiceRestriction
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }

        //cant buy on that date
        public ServiceDateRestriction(int year, int month, int day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }
    }
}
