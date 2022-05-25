namespace WebAPI.Requests
{
    public class ChangeProductAmountInStoreRequest : StoreManagementRequest
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public ChangeProductAmountInStoreRequest(int userId, int storeId, int productId, int amount) :
            base(userId, storeId)
        {
            ProductId = productId;
            Amount = amount;
        }
    }
}
