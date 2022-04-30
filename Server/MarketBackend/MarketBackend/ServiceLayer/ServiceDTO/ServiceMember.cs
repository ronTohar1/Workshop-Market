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
        private IList<string> notifications;
        
        // need to change that when adding fields to member
        public ServiceMember(Member m)
        {
            userName = string.Empty;
            loggedIn = false;
            notifications = new List<string>();
        }
    }
}
