using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer.ServiceDTO;
using MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO;
using MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs;

namespace MarketBackend.ServiceLayer
{
    public interface IStoreManagementFacade
    {
        Response<int> AddDiscountPolicy(ServiceExpression expression, string description, int storeId, int memberId);
        Response<int> AddNewProduct(int userId, int storeId, string productName, double price, string category);
        Response<bool> AddProductToInventory(int userId, int storeId, int productId, int amount);
        Response<int> AddPurchasePolicy(ServiceDTO.PurchaseDTOs.ServicePurchasePolicy expression, string description, int storeId, int memberId);
        Response<bool> ChangeManagerPermission(int userId, int targetUserId, int storeId, IList<Permission> permissions);
        Response<bool> CloseStore(int userId, int storeId);
        Response<bool> DecreaseProduct(int userId, int storeId, int productId, int amount);
        Response<ServiceMember> GetFounder(int storeId, int memberId);
        Response<IList<Permission>> GetManagerPermissions(int storeId, int requestingMemberId, int managerMemberId);
        Response<IList<int>> GetMembersInRole(int storeId, int memberId, Role role);
        Response<IList<Purchase>> GetPurchaseHistory(int userId, int storeId);
        Response<bool> MakeCoManager(int userId, int targetUserId, int storeId);
        Response<bool> MakeCoOwner(int userId, int targetUserId, int storeId);
        Response<int> OpenNewStore(int userId, string storeName);
        Response<bool> RemoveCoOwner(int userId, int targetUserId, int storeId);
        Response<bool> RemoveDiscountPolicy(int disId, int storeId, int memberId);
        Response<bool> RemovePurchasePolicy(int policyId, int storeId, int memberId);
    }
}