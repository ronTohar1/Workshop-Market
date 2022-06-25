using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer.ServiceDTO;
using MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO;
using MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs;

namespace MarketBackend.ServiceLayer
{
    public interface IStoreManagementFacade
    {
        Response<int> AddBid(int storeId, int productId, int memberId, double bidPrice);
        Response<bool> ApproveBid(int storeId, int memberId, int bidId);
        Response<bool> DenyBid(int storeId, int memberId, int bidId);
        Response<bool> ApproveCounterOffer(int storeId, int memberId, int bidId);
        Response<bool> DenyCounterOffer(int storeId, int memberId, int bidId);
        Response<bool> RemoveBid(int storeId, int memberId, int bidId);
        Response<bool> MakeCounterOffer(int storeId, int memberId, int bidId, double offer);
        Response<IList<int>> GetApproveForBid(int storeId, int memberId, int bidId);

        Response<int> AddDiscountPolicy(ServiceExpression expression, string description, int storeId, int memberId);
        Response<int> AddNewProduct(int userId, int storeId, string productName, double price, string category);
        Response<bool> AddProductReview(int storeId, int memberId, int productId, string review);
        Response<bool> AddProductToInventory(int userId, int storeId, int productId, int amount);
        Response<int> AddPurchasePolicy(ServiceDTO.PurchaseDTOs.ServicePurchasePolicy expression, string description, int storeId, int memberId);
        Response<bool> ChangeManagerPermission(int userId, int targetUserId, int storeId, IList<Permission> permissions);
        Response<bool> CloseStore(int userId, int storeId);
        Response<bool> DecreaseProduct(int userId, int storeId, int productId, int amount);
        Response<ServiceMember> GetFounder(int storeId, int memberId);
        Response<IList<Permission>> GetManagerPermissions(int storeId, int requestingMemberId, int managerMemberId);
        Response<IList<int>> GetMembersInRole(int storeId, int memberId, Role role);
        Response<IDictionary<string, IList<string>>> GetProductReviews(int storeId, int productId);
        Response<IList<Purchase>> GetPurchaseHistory(int userId, int storeId);
        Response<bool> MakeCoManager(int userId, int targetUserId, int storeId);
        Response<bool> MakeCoOwner(int userId, int targetUserId, int storeId);
        Response<bool> DenyNewCoOwner(int userId, int targetUserId, int storeId);
        // Response<int> OpenStore(int userId, string storeName);
        Response<int> OpenNewStore(int userId, string storeName);
        Response<bool> RemoveCoOwner(int userId, int targetUserId, int storeId);
        Response<bool> RemoveDiscountPolicy(int disId, int storeId, int memberId);
        Response<bool> RemovePurchasePolicy(int policyId, int storeId, int memberId);
        Response<double> GetStoreDailyProfit(int storeId, int memberId);
        Response<IList<ServiceBid>> GetAllMemberBids(int memberId);
        Response<bool> MakeNewCoOwner(int userId, int targetUserId, int storeId);
    }
}