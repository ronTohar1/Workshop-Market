namespace WebAPI.Requests
{
    public class OpenStoreRequest : UserRequest
    {
        public string StoreName { get; set; }

        public OpenStoreRequest(int userId, string storeName) : base(userId) => StoreName = storeName;
    }
}
