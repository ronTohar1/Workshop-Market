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
        private readonly MembersController membersController;

        MemberFacade(MembersController membersController)
        {
            this.membersController = membersController;
        }

        Response<int> Register(string userName, string password)
        {
            //membersController.
            return new Response<int>();
        }

        Response<int> Login(string userName, string password)
        {
            return new Response<int>();
        }

        Response<bool> Logout(int memberId)
        {
            return new Response<bool>();
        }

        Response<bool> AddProductReview(int memberId, int storeId, int productId, string review)
        {
            return new Response<bool>();
        }

    }
}
