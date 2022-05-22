namespace WebAPI.Requests
{
    public class OpenMarketRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public OpenMarketRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
