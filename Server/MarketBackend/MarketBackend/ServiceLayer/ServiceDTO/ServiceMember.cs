using MarketBackend.BusinessLayer.Buyers.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{

    internal class ServiceMember
    {
        public string UserName { get; }
        public bool LoggedIn { get;  }
        
        // need to change that when adding fields to member
        public ServiceMember(Member m)
        {
            UserName = m.Username;
            LoggedIn = m.LoggedIn;
        }
    }
}
