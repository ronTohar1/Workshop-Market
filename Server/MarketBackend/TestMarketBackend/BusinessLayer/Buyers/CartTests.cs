using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    public class CartTests
    {
        private Cart cart;

        
        // ----------- Setup helping functions -----------------------------

        [SetUp]
        private void Setup()
        {
            cart = new Cart();

        }
        
        [Test]
        //[TestCase(coOwnerId1, memberId1)]
        public void TestMakeCoOwnerSholdPass(int requestingMemberId, int newCoOwnerMemberId)
        {
            //SetupStoreNoRoles();

            //store.MakeCoOwner(founderMemberId, requestingMemberId); // this is part of the testing
            //Assert.IsTrue(store.IsCoOwner(requestingMemberId));
            
            //store.MakeCoOwner(requestingMemberId, newCoOwnerMemberId);
            //Assert.IsTrue(store.IsCoOwner(newCoOwnerMemberId));
        }
    }
}