namespace WebAPI.Requests
{
    public class UserRequest
    {
        public int UserId { get; set; }

        public UserRequest(int userId) => UserId = userId;
    }
}
