using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers.Members
{
    internal class Security
    {

        public Security()
        {

        }

        public int HashPassword(string password)
        {
            return password.GetHashCode();
        }


    }
}
