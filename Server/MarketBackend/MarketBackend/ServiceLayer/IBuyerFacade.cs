using MarketBackend.ServiceLayer.ServiceDTO;

namespace MarketBackend.ServiceLayer
{
    public interface IBuyerFacade
    {
        Response<bool> PurchaseBid(int storeId, int bidId, int memberId, ServicePaymentDetails paymentDetails, ServiceSupplyDetails supplyDetails);

        Response<bool> AddProdcutToCart(int userId, int storeId, int productId, int amount);
        Response<bool> AddProductReview(int memberId, int storeId, int productId, string review);
        Response<bool> changeProductAmountInCart(int userId, int storeId, int productId, int amount);
        Response<int> Enter();
        Response<ServiceCart> GetCart(int userId);
        Response<ServiceStore> GetStoreInfo(int storeId);
        Response<ServiceStore> GetStoreInfo(string storeName);
        Response<bool> Leave(int userId);
        Response<int> Login(string userName, string password, Func<string[], bool> notifier);
        Response<bool> Logout(int memberId);
        Response<IDictionary<int, IList<ServiceProduct>>> ProductsSearch(string? storeName = null, string? productName = null, string? category = null, string? keyword = null, int? productId = null, IList<int> productIds = null, ServiceMemberInRole memberInRole = null, bool storesWithProductsThatPassedFilter = true);
        Response<ServicePurchase> PurchaseCartContent(int userId, ServicePaymentDetails paymentDetails, ServiceSupplyDetails supplyDetails);
        Response<int> Register(string userName, string password);
        Response<bool> RemoveProductFromCart(int userId, int storeId, int productId);
    }
}