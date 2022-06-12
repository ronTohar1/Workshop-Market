namespace WebAPI.Requests
{
    public class AddProductReviewRequest
    {
        public int StoreId { get; set; }
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public string Review { get; set; }
        public AddProductReviewRequest(int storeId, int memberId, int productId, string review)
        {
            StoreId = storeId;
            MemberId = memberId;
            ProductId = productId;
            Review = review;
        }
    }
}
