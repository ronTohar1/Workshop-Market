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
        private string userName;
        private bool loggedIn;
        
        // need to change that when adding fields to member
        public ServiceMember(Member m)
        {
            userName = m.Username;
            loggedIn = m.LoggedIn;
        }
    }
}
