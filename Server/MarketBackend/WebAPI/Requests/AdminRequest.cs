namespace WebAPI.Requests
{
    public class AdminRequest : UserRequest
    {
        public int TargetId { get; set; }

        public AdminRequest(int adminId, int targetId) : base(adminId) => TargetId = targetId;
    }
}
