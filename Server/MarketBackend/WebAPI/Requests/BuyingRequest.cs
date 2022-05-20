namespace WebAPI.Requests
{
    public class BuyingRequest : UserRequest
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
    }
}
