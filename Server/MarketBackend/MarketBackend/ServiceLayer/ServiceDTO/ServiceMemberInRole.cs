using MarketBackend.BusinessLayer.Market.StoreManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServiceMemberInRole
    {
        public int MemberId { get; set; }
        public Role RoleInStore { get; set; }

        public ServiceMemberInRole(int memberId, Role roleInStore)
        {
            MemberId = memberId;
            RoleInStore = roleInStore;
        }

        public override bool Equals(Object? other)
        {
            if (other == null || !(other is ServiceMemberInRole))
                return false;
            ServiceMemberInRole otherMIR = (ServiceMemberInRole)other;
            return this.MemberId == otherMIR.MemberId && this.RoleInStore == otherMIR.RoleInStore; 
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MemberId, RoleInStore.GetHashCode()); // todo: make sure this is okay
        }
    }
}
