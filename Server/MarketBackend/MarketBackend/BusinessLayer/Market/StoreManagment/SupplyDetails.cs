using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class SupplyDetails
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }

        public SupplyDetails(string name, string address, string city, string country, string zip)
        {
            Name = name;
            Address = address;
            City = city;
            Country = country;
            Zip = zip;
        }

    }
}
