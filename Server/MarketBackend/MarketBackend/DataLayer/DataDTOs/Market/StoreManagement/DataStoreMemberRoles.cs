using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.DataLayer.DataDTOs.Buyers;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement
{
    public class DataStoreMemberRoles
    {
        public int StoreId { get; set; }
        public int MemberId { get; set; }
        public Role Role { get; set; }

        public IList<DataManagerPermission> ManagerPermissions { get; set; }
    }
}