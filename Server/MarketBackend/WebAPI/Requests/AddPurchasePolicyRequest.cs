using MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs;

namespace WebAPI.Requests
{
    public class AddPurchasePolicyRequest : StoreManagementRequest
    {
        public string Expression { get; set; }
        public string Description { get; set; }

        public AddPurchasePolicyRequest(int userId, int storeId, string expression, string description) :
            base(userId, storeId)
        {
            Expression = expression;
            Description = description;
        }
    }
}
