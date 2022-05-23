namespace WebAPI.Requests
{
    public class AddProductToCartRequest : BuyingRequest
    {
        public int Amount { get; set; }

        public AddProductToCartRequest(int userId, int productId, int storeId, int amount) : 
            base(userId, productId, storeId) => Amount = amount;
    }
}
