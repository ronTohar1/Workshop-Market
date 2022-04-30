using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend.BusinessLayer.Market
{
    public class PurchasesManagerTetsts
    {
        private StoreController storeController;
        private Mock<StoreController> storeControllerMock;

        private BuyersController buyersController;
        private Mock<BuyersController> buyersControllerMock;

        private Cart cart;
        private Mock<Cart> cartMock;

        
        private Store store1;
        private Mock<Store> storeMock1;
        private Store store2;
        private Mock<Store> storeMock2;
        private Func<int, Member> memberGetter;
        private const string storeName1 = "Amazon";
        private const string storeName2 = "Ebay";

        // ------- Setup helping functions -------------------------------------
        
        
        private void buyersConrtollerBuyersExistsSetup(int[] exisitingBuyersIds)
        {
            buyersControllerMock = new Mock<BuyersController>();
            cartMock = new Mock<Cart>(); 
            cart = cartMock.Object;

            // returns mock cart for every buyer Id
            foreach (int existingBuyerId in exisitingBuyersIds)
            {
                buyersControllerMock.Setup(buyersController =>
                buyersController.GetCart(It.Is<int>(id => id == existingBuyerId))).
                    Returns(cart);

                buyersControllerMock.Setup(buyersController =>
                buyersController.BuyerAvailable(It.Is<int>(id => id == existingBuyerId))).
                    Returns(true);
            }
            buyersControllerMock.Setup(buyersController =>
                buyersController.BuyerAvailable(It.Is<int>(id => !exisitingBuyersIds.Contains(id)))).
                    Returns(false);
        }
        private void storeConrtollerSetup()
        {
            // store simple setup
            Mock<Member> memberMock = new Mock<Member>("user123", 12345678);
            Member founderOfBothStores = memberMock.Object;

            memberGetter = memberId =>
            {
                if (founderOfBothStores.Id == memberId)
                    return founderOfBothStores;
                
                return null;
            };


            storeControllerMock = new Mock<StoreController>();
            storeMock1 = new Mock<Store>();
            store1 = storeMock1.Object;

            

    }
}
