using MarketBackend.ServiceLayer.ServiceDTO;

namespace MarketBackend.ServiceLayer
{
    public interface ISystemOperator
    {
        Response<bool> CloseMarket(bool clearDatabase);
        Response<AdminFacade> GetAdminFacade();
        Response<BuyerFacade> GetBuyerFacade();
        Response<ExternalSystemFacade> GetExternalSystemFacade();
        Response<StoreManagementFacade> GetStoreManagementFacade();
        Response<int> OpenMarket(string username, string password, bool loadDatabase);
    }
}