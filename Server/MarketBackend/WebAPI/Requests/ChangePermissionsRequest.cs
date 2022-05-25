using MarketBackend.BusinessLayer.Market.StoreManagment;

namespace WebAPI.Requests
{
    public class ChangePermissionsRequest : RolesManagementRequest
    {
        public IList<Permission> Permissions { get; set; }

        public ChangePermissionsRequest(int userId, int storeId, int targetUserId, IList<Permission> permissions) :
            base(userId, storeId, targetUserId) => Permissions = permissions;
    }
}
