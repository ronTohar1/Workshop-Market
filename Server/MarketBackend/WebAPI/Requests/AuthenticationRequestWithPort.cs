namespace WebAPI.Requests
{
    public class AuthenticationRequestWithPort
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
    }
}
