namespace WebAPI.Requests
{
    public class AdminRequest : UserRequest
    {
        public int TargetId { get; set; }

        public AdminRequest(int userId, int targetId) : base(userId) => TargetId = targetId;
    }
}
