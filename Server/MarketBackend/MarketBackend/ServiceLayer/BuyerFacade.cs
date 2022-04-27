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

    }
}
