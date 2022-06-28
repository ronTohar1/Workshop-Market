using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.DataLayer.DataDTOs.Buyers;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement
{
    public class DataStoreMemberRoles
    {
        public int Id { get; set; }
        public virtual DataStore? Store { get; set; }
        public int MemberId { get; set; }
        public Role Role { get; set; }

        public virtual IList<DataManagerPermission?>? ManagerPermissions { get; set; }
    }
}