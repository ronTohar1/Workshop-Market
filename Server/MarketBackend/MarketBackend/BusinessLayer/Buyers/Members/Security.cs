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

        public bool CheckUsername(string username)
        {
            return true;
        }

        public bool CheckPassword(string password)
        {
            return true;
        }

        public int HashPassword(string password)
        {
            return password.GetHashCode();
        }


    }
}
