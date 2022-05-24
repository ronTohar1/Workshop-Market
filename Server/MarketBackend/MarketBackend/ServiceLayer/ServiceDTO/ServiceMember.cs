using MarketBackend.BusinessLayer.Buyers.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{

    public class ServiceMember
    {
        public int Id { get; }
        public string UserName { get; }
        public bool LoggedIn { get;  }
        
        // need to change that when adding fields to member
        public ServiceMember(int memberId, Member m)
        {
            Id = memberId;
            UserName = m.Username;
            LoggedIn = m.LoggedIn;
        }

        public ServiceMember(int memberId, string username, bool loggedIn)
        {
            Id = memberId;
            UserName = username;
            LoggedIn = loggedIn;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ServiceMember otherServiceMember = (ServiceMember)obj;
            return otherServiceMember.UserName == this.UserName && otherServiceMember.LoggedIn == this.LoggedIn;
        }
    }
}
