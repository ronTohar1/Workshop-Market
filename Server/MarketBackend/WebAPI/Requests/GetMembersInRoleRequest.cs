using MarketBackend.BusinessLayer.Market.StoreManagment;

namespace WebAPI.Requests
{
    public class GetMembersInRoleRequest : StoreManagementRequest
    {
        public Role Role { get; set; }

        public GetMembersInRoleRequest(int userId, int storeId, Role role) : 
            base(userId, storeId) => Role = role;
    }
}
