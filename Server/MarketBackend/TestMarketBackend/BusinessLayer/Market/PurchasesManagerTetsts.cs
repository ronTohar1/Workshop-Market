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
using MarketBackend.BusinessLayer;

namespace TestMarketBackend.BusinessLayer.Market
{
    public class PurchasesManagerTetsts
    {
        private PurchasesManager purchasesManager; 

        private StoreController storeController;
        private Mock<StoreController> storeControllerMock;

        private BuyersController buyersController;
        private Mock<BuyersController> buyersControllerMock;

        private ExternalServicesController externalServicesController;
        private Mock<ExternalServicesController> externalServicesControllerMock;

        private bool addedToCart;
        private bool removedFromCart; 

        private const int buyerId1 = 1;
        private const int notABuyerId1 = 2;

        private const int storeYesId1 = 11;
        private const int storeNoId1 = 12;
        private const int notAStoreId1 = 13;

        private const int productId1 = 1; 



        // todo: decide if to remove with the next lines

        //private Cart cart;
        //private Mock<Cart> cartMock;
       
        //private Store store1;
        //private Mock<Store> storeMock1;
        //private Store store2;
        //private Mock<Store> storeMock2;
        //private Func<int, Member> memberGetter;
        //private const string storeName1 = "Amazon";
        //private const string storeName2 = "Ebay";

        // ------- Setup helping functions -------------------------------------

        private Cart MockCart()
        {
            Mock<Cart> cartMock = new Mock<Cart>();

            cartMock.Setup(cart =>
                    cart.AddProductToCart(It.IsAny<ProductInBag>(), It.IsAny<int>())).
                        Callback(() => { addedToCart = true;  });
            cartMock.Setup(cart =>
                    cart.RemoveProductFromCart(It.IsAny<ProductInBag>())).
                        Callback(() => { removedFromCart = true; });

            return cartMock.Object;
        }

        private Buyer MockBuyer()
        {
            Mock<Buyer> buyerMock = new Mock<Buyer>();

            Cart cart = MockCart(); 

            buyerMock.Setup(buyer =>
                    buyer.Cart).
                        Returns(cart);

            return buyerMock.Object;
        }

        private void BuyersControllerSetup()
        {
            buyersControllerMock = new Mock<BuyersController>();

            Buyer mockedBuyer = MockBuyer(); 

            buyersControllerMock.Setup(buyersController =>
                    buyersController.GetBuyer(It.Is<int>(id => id == buyerId1))).
                        Returns(mockedBuyer);
            buyersControllerMock.Setup(buyersController =>
                    buyersController.GetBuyer(It.Is<int>(id => id == notABuyerId1))).
                        Returns((Buyer)null);

            buyersController = buyersControllerMock.Object; 
        }

        private Store MockStoreThatReturns(string result)
        {
            Mock<Member> founderMock = new Mock<Member>("user123", 12345678); // todo: check if okay
            Member founder = founderMock.Object;

            Mock<Store> storeMock = new Mock<Store>("store1", founder, (int id) => (Member)null);

            storeMock.Setup(store =>
                    store.CanBuyProduct(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).
                        Returns(result);

            return storeMock.Object;
        }

        private void StoreControllerSetup()
        {
            storeControllerMock = new Mock<StoreController>(null); // sending null MembersController

            Store mockedYesStore = MockStoreThatReturns(null); // means yes
            Store mockedNoStore = MockStoreThatReturns("Product not in store"); // means no

            storeControllerMock.Setup(storeController =>
                    storeController.GetOpenStore(It.Is<int>(id => id == storeYesId1))).
                        Returns(mockedYesStore);
            storeControllerMock.Setup(storeController =>
                    storeController.GetOpenStore(It.Is<int>(id => id == storeNoId1))).
                        Returns(mockedNoStore);
            storeControllerMock.Setup(storeController =>
                    storeController.GetOpenStore(It.Is<int>(id => id == notAStoreId1))).
                        Returns((Store)null);

            storeController = storeControllerMock.Object;
        }

        private void ExternalServicesControllerSetup()
        {
            externalServicesControllerMock = new Mock<ExternalServicesController>(null, null); // initialized external services

            externalServicesController = externalServicesControllerMock.Object;
        }

        [SetUp]
        public void PassedValuesInitialization()
        {
            addedToCart = false;
            removedFromCart = false; 
        }

        [SetUp]
        public void FullPurchasesManagerSetup()
        {
            BuyersControllerSetup();
            StoreControllerSetup();
            ExternalServicesControllerSetup();

            purchasesManager = new PurchasesManager(storeController, buyersController, externalServicesController);
        }


        // todo: decide if to remove the next lines

        //private void buyersConrtollerBuyersExistsSetup(int[] exisitingBuyersIds)
        //{
        //    buyersControllerMock = new Mock<BuyersController>();
        //    cartMock = new Mock<Cart>(); 
        //    cart = cartMock.Object;

        //    // returns mock cart for every buyer Id
        //    foreach (int existingBuyerId in exisitingBuyersIds)
        //    {
        //        buyersControllerMock.Setup(buyersController =>
        //        buyersController.GetCart(It.Is<int>(id => id == existingBuyerId))).
        //            Returns(cart);

        //        buyersControllerMock.Setup(buyersController =>
        //        buyersController.BuyerAvailable(It.Is<int>(id => id == existingBuyerId))).
        //            Returns(true);
        //    }
        //    buyersControllerMock.Setup(buyersController =>
        //        buyersController.BuyerAvailable(It.Is<int>(id => !exisitingBuyersIds.Contains(id)))).
        //            Returns(false);
        //}
        //private void storeConrtollerSetup()
        //{
        //    // store simple setup
        //    Mock<Member> memberMock = new Mock<Member>("user123", 12345678);
        //    Member founderOfBothStores = memberMock.Object;

        //    memberGetter = memberId =>
        //    {
        //        if (founderOfBothStores.Id == memberId)
        //            return founderOfBothStores;

        //        return null;
        //    };


        //    storeControllerMock = new Mock<StoreController>();
        //    storeMock1 = new Mock<Store>();
        //    store1 = storeMock1.Object;

        //}

        // ------- AddProductToCart() ----------------------------------------

        // buyer does not exist
        // store does not exist
        // store says no to action (all its reasons is its responsibility, its just mocked here, 
        //                            including product does not exist, etc. )
        // negative, zero amouts: for informative message
        
        // should pass tests

        [Test]
        [TestCase(notABuyerId1, storeYesId1, productId1, 10, false)]
        [TestCase(buyerId1, notAStoreId1, productId1, 10, false)]
        [TestCase(buyerId1, storeNoId1, productId1, 10, true)]
        [TestCase(buyerId1, storeYesId1, productId1, 0, true)]
        [TestCase(buyerId1, storeYesId1, productId1, -1, true)]
        public void TestAddProductToCartSholdFail(int buyerId, int storeId, int productId, int amount, bool isUserError)
        {
            if (isUserError)
                Assert.Throws<MarketException>(() => purchasesManager.AddProductToCart(buyerId, storeId, productId, amount));
            else
                Assert.Throws<ArgumentException>(() => purchasesManager.AddProductToCart(buyerId, storeId, productId, amount));
        }

        [Test]
        [TestCase(buyerId1, storeYesId1, productId1, 10)]
        [TestCase(buyerId1, storeYesId1, productId1, 130)]
        public void TestAddProductToCartSholdPass(int buyerId, int storeId, int productId, int amount)
        {
            purchasesManager.AddProductToCart(buyerId, storeId, productId, amount);

            Assert.IsTrue(addedToCart); // cart is mocked to change this
        }

        // ------- RemoveProductFromCart() ----------------------------------------

        // almost same as above
        // (product is already not in bag)
        // doesn't need to check if CanBuyProduct

        [Test]
        [TestCase(notABuyerId1, storeYesId1, productId1, 10, false)]
        [TestCase(buyerId1, notAStoreId1, productId1, 10, false)]
        [TestCase(buyerId1, storeYesId1, productId1, 0, true)]
        [TestCase(buyerId1, storeYesId1, productId1, -1, true)]
        public void TestRemoveProductFromCartSholdFail(int buyerId, int storeId, int productId, int amount, bool isUserError)
        {
            if (isUserError)
                Assert.Throws<MarketException>(() => purchasesManager.RemoveProductFromCart(buyerId, storeId, productId, amount));
            else
                Assert.Throws<ArgumentException>(() => purchasesManager.RemoveProductFromCart(buyerId, storeId, productId, amount));
        }

        [Test]
        [TestCase(buyerId1, storeYesId1, productId1, 10)]
        [TestCase(buyerId1, storeYesId1, productId1, 130)]
        public void TestRemoveProductFromCartSholdPass(int buyerId, int storeId, int productId, int amount)
        {
            purchasesManager.RemoveProductFromCart(buyerId, storeId, productId, amount);

            Assert.IsTrue(removedFromCart); // cart is mocked to change this
        }

    }
}
