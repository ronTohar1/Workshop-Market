namespace WebAPI.Requests
{
    public class BuyingRequest : UserRequest
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }

        public BuyingRequest(int userId, int productId, int storeId) : base(userId)
        {
            ProductId = productId;
            StoreId = storeId;
        }
    }
}
