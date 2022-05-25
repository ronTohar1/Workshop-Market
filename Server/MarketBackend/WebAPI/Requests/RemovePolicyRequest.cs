namespace WebAPI.Requests
{
    public class RemovePolicyRequest : StoreManagementRequest
    {
        public int PolicyId { get; set; }

        public RemovePolicyRequest(int userId, int storeId, int policyId) : 
            base(userId, storeId) => PolicyId = policyId;
    }
}
