using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers.Members
{
    public class Member
    {
        public string name { get; }

        public virtual int GetId()
        {
            return -1; // todo: implement
        }
    }
}
