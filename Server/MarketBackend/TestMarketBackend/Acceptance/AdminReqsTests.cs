using MarketBackend.ServiceLayer.ServiceDTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend.Acceptance
{
    internal class AdminReqsTests : AcceptanceTests
    {
        //// r 6.4
        //[Test]
        //[TestCase(storeName)]
        //public void FailedGetStorePurchaseHistory(string storeName)
        //{
        //    int storeId = GetStoreIdByName(storeName);
        //    Response<IList<ServicePurchase>> response = adminFacade.GetStorePurchaseHistory(adminId, storeId); 

        //    Assert.IsTrue(!response.ErrorOccured());
        //    Assert.Is
        //    // and more
        //}

        private int GetStoreIdByName(string storeName)
        {
            Response<ServiceStore> response = buyerFacade.GetStoreInfo(storeName);
            if (response.ErrorOccured())
                throw new Exception(response.ErrorMessage);
            return response.Value.Id; 
        }
    }
}
