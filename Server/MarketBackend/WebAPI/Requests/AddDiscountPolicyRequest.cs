using MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO;

namespace WebAPI.Requests
{
    public class AddDiscountPolicyRequest : StoreManagementRequest
    {
        public IServiceExpression Expression { get; set; }
        public string Description { get; set; }

        public AddDiscountPolicyRequest(int userId, int storeId, IServiceExpression expression, string description) :
            base(userId, storeId)
        {
            Expression = expression;
            Description = description;
        }
    }
}
