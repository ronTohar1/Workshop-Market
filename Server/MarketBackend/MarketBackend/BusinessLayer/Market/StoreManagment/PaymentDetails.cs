using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class PaymentDetails
    {
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Holder { get; set; }
        public string Ccv { get; set; }
        public string Id { get; set; }

        public PaymentDetails(string cardNumber, string month, string year, string holder, string ccv, string id)
        {
            CardNumber = cardNumber;
            Month = month;
            Year = year;
            Holder = holder;
            Ccv = ccv;
            Id = id;
        }
    }
}
