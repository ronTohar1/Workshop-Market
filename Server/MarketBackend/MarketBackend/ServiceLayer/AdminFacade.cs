using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer
{
    internal class AdminFacade
    {
        
        public Response<IList<Purchase>> GetPurchaseHistory(int currUserId, int storeId)
        {
            return new Response<IList<Purchase>>();
        }

    }
}
