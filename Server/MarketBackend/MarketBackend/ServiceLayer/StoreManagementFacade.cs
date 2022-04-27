using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer
{
    internal class StoreManagementFacade
    {
        public Response<int> AddNewProduct(int userId, int storeId, string productName, double price)
        {
            return new Response<int>();
        }

        public Response<bool> AddProductToInventory(int userId, int storeId, int productId, int amount)
        {
            return new Response<bool>();
        }

        public Response<bool> DecreaseProduct(int userId, int storeId, int productId, int amount)
        {
            return new Response<bool>();
        }

        public Response<bool> AddPurchasePolicy(PurchaseOption type, int userId, int storeId)
        {
            return new Response<bool>();
        }

        public Response<bool> RemovePurchasePolicy(PurchaseOption type, int userId, int storeId)
        {
            return new Response<bool>();
        }

        public Response<bool> AddDiscountPolicy(string discountCode, int userId, int storeId, int productId, double discount)
        {
            return new Response<bool>();
        }

        public Response<bool> RemoveDiscountPolicy(string discountCode, int userId, int storeId, int productId, double discount)
        {
            return new Response<bool>();
        }

        public Response<bool> MakeCoOwner(int userId, int targetUserId, int storeId)
        {
            return new Response<bool>();
        }

        public Response<bool> MakeCoManager(int userId, int targetUserId, int storeId)
        {
            return new Response<bool>();
        }

        public Response<bool> ChangeManagerPermission(int userId, int targetUserId, int storeId, ICollection<Permission> permissions)
        {
            return new Response<bool>();
        }

        public Response<IDictionary<int, ICollection<Permission>>> GetInfoAboutRoles(int userId, int storeId)
        {
            return new Response<IDictionary<int, ICollection<Permission>>>();
        }

        public Response<IList<Purchase>> GetPurchaseHistory(int userId, int storeId)
        {
            return new Response<IList<Purchase>>();
        }

        public Response<int> OpenStore(int userId, string storeName)
        {
            return new Response<int>();
        }

    }
}
