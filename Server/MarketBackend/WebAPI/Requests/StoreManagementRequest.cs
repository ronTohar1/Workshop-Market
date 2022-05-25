namespace WebAPI.Requests
{
    public class StoreManagementRequest : UserRequest
    {
        public int StoreId { get; set; }

        public StoreManagementRequest(int userId, int storeId) : base(userId) => StoreId = storeId;
    }
}
