using MarketBackend.ServiceLayer.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer
{
    internal class GuestFacade : BuyerFacade
    {
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
    }
}
