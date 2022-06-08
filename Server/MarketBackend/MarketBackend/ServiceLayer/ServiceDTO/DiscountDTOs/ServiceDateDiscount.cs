using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceDateDiscount : ServiceStoreDiscount
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }

        public ServiceDateDiscount(int discount, int year = -1, int month = -1, int day = -1, string tag = "") : base(discount, tag)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }
    }
}
