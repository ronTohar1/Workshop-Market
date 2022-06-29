using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers.Members
{
    public class Security
    {

        public Security()
        {

        }

        public int HashPassword(string password)
        {
            SHA256 sha = SHA256.Create();
            var hashed = sha.ComputeHash(Encoding.UTF8.GetBytes(password + "OurCoolSalt"));
            return BitConverter.ToInt32(hashed, 0);
        }

    }
}
