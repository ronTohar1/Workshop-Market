namespace WebAPI.Requests
{
    public class GetProductReviewsRequest
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }

        public GetProductReviewsRequest(int storeId, int productId)
        {
            StoreId = storeId;
            ProductId = productId;
        }
    }
}
