using MarketBackend.ServiceLayer.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer
{
    internal class BuyerFacade
    {
        public Response<ServiceCart> GetCart(int userId)
        {
            return new Response<ServiceCart>();
        }

        public Response<bool> AddProdcutToCart(int userId, int storeId, int productId, int amount)
        {
            return new Response<bool>();
        }

        public Response<bool> RemoveProductFromCart(int userId, int storeId, int productId, int amount)
        {
            return new Response<bool>();
        }

        public Response<bool> PurchaseCartContent(int userId)
        {
            return new Response<bool>();
        }

        public Response<int> Enter()
        {
            return new Response<int>();
        }

        public Response<bool> Leave()
        {
            return new Response<bool>();
        }
        public Response<ServiceStore> GetStoreInfo(int storeId)
        {
            return new Response<ServiceStore>();
        }

        public Response<ServiceStore> GetStoreInfo(string storeName)
        {
            return new Response<ServiceStore>();
        }

        public Response<IList<ServiceProduct>> ProductsSearch(ProductFilter filter, int numberToLoad, int fromIndex)
        {
            return new Response<IList<ServiceProduct>>();
        }

        public Response<int> Register(string userName, string password)
        {
            return new Response<int>();
        }

        public Response<int> Login(string userName, string password)
        {
            return new Response<int>();
        }

        public Response<bool> Logout(int memberId)
        {
            return new Response<bool>();
        }

        public Response<bool> AddProductReview(int memberId, int storeId, int productId, string review)
        {
            return new Response<bool>();
        }

    }
}
