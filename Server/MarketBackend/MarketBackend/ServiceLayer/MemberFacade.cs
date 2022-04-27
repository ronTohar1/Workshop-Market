using MarketBackend.ServiceLayer.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer
{
    internal class MemberFacade : BuyerFacade
    {

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
