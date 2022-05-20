namespace WebAPI.Requests
{
    public class ReviewProductRequest : UserRequest
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public string Review { get; set; }

        public ReviewProductRequest(int userId, int storeId, int productId, string review) : base(userId)
        {
            StoreId = storeId;
            ProductId = productId;
            Review = review;
        }
    }
}
