namespace WebAPI.Requests
{
    public class RolesManagementRequest : StoreManagementRequest
    {
        public int TargetUserId { get; set; }

        public RolesManagementRequest(int userId, int storeId, int targetUserId) : 
            base(userId, storeId) => TargetUserId = targetUserId;
    }
}
