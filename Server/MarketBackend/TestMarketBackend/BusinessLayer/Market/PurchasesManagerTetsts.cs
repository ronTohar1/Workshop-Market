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
using MarketBackend.BusinessLayer.System.ExternalServices;
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

        private const int storeYesId2 = 14;
        private const int productId1 = 1;
        private const int productId2 = 2;
        private const int productId3 = 3;
        private const int amount1 = 4;
        private const int amount2 = 5;
        private const int amount3 = 6;

        private const double totalPrice = 99.9;

        private int counter;
        private bool removeFromStore1FromCart;
        private bool removeFromStore2FromCart;

        private IDictionary<int, IList<Tuple<int, int>>> case1legal = new Dictionary<int, IList<Tuple<int, int>>>()
        {
            [storeYesId1] = new List<Tuple<int, int>>() { new Tuple<int, int> (productId1, amount1), new Tuple<int, int>(productId2, amount1) },
            [storeYesId2] = new List<Tuple<int, int>>() { new Tuple<int, int>(productId1, amount2), new Tuple<int, int>(productId2, amount3) },
        };
        private IDictionary<int, IList<Tuple<int, int>>> case2legal = new Dictionary<int, IList<Tuple<int, int>>>()
        { 
            [storeYesId1] = new List<Tuple<int, int>>() { new Tuple<int, int>(productId1, amount2), new Tuple<int, int>(productId2, amount3) },
        };
        private IDictionary<int, IList<Tuple<int, int>>> case3illegal = new Dictionary<int, IList<Tuple<int, int>>>()
        {
            [storeYesId1] = new List<Tuple<int, int>>() { new Tuple<int, int>(productId1, amount1), new Tuple<int, int>(productId2, amount1) },
            [storeNoId1] = new List<Tuple<int, int>>() { new Tuple<int, int>(productId1, amount2), new Tuple<int, int>(productId2, amount3) },
        };
        private IDictionary<int, IList<Tuple<int, int>>> case4illegal = new Dictionary<int, IList<Tuple<int, int>>>()
        {
            [notAStoreId1] = new List<Tuple<int, int>>() { new Tuple<int, int>(productId1, amount1), new Tuple<int, int>(productId2, amount1) }
        };



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
            Mock<Security> securityMock = new Mock<Security>();
            Mock<Member> founderMock = new Mock<Member>("user123", "12345678", securityMock.Object); // todo: check if okay
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
            externalServicesControllerMock = new Mock<ExternalServicesController>(new ExternalPaymentSystem(), new ExternalSupplySystem()); // initialized external services

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
        
        
        // from here purchase managment tests
      
        private Cart MockCart2()
        {
            Mock<Cart> cartMock = new Mock<Cart>();

            cartMock.Setup(cart =>
                    cart.GetProductInBag(It.Is<int>(id => id == storeYesId1), It.IsAny<int>())).
                        Returns((ProductInBag)null).Callback(()=>removeFromStore1FromCart=true);
            cartMock.Setup(cart =>
                    cart.GetProductInBag(It.Is<int>(id => id == storeYesId2), It.IsAny<int>())).
                        Returns((ProductInBag)null).Callback(() => removeFromStore2FromCart = true);
            cartMock.Setup(cart =>
                    cart.RemoveProductFromCart(It.IsAny<ProductInBag>())).
                        Callback(() => { removedFromCart = true; });

            return cartMock.Object;
        }
        private Buyer MockBuyer2()
        {
            Mock<Buyer> buyerMock = new Mock<Buyer>();

            Cart cart = MockCart2();

            buyerMock.Setup(buyer =>
                    buyer.Cart).
                        Returns(cart);
            buyerMock.Setup(buyer =>
                    buyer.AddPurchase(It.IsAny<Purchase>()));
            return buyerMock.Object;
        }
        private void BuyersControllerSetup2()
        {
            buyersControllerMock = new Mock<BuyersController>();

            Buyer mockedBuyer = MockBuyer2();

            buyersControllerMock.Setup(buyersController =>
                    buyersController.GetBuyer(It.Is<int>(id => id == buyerId1))).
                        Returns(mockedBuyer);
            buyersControllerMock.Setup(buyersController =>
                    buyersController.GetBuyer(It.Is<int>(id => id == notABuyerId1))).
                        Returns((Buyer)null);

            buyersController = buyersControllerMock.Object;
        }

        private Store MockStoreThatReturns2(string result)
        {
            Mock<Security> securityMock = new Mock<Security>();
            Mock<Member> founderMock = new Mock<Member>("user123", "12345678", securityMock.Object); // todo: check if okay
            Member founder = founderMock.Object;

            Mock<Store> storeMock = new Mock<Store>("store1", founder, (int id) => (Member)null);

            Mock<Product> productMock = new Mock<Product>("cheese", 5.90, "dairy");

            storeMock.Setup(store =>
                    store.DecreaseProductAmountFromInventory(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).
                        Callback(() => counter++);

            storeMock.Setup(store =>
                    store.CanBuyProduct(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).
                        Returns(result);
            storeMock.Setup(store =>
                   store.SearchProductByProductId(It.IsAny<int>())).
                       Returns(productMock.Object);
            storeMock.Setup(store =>
                   store.AddPurchaseRecord(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<double>(), It.IsAny<string>()));
            storeMock.Setup(store =>
                   store.GetTotalBagCost(It.IsAny<IDictionary<int,int>>())).Returns(totalPrice);

            return storeMock.Object;
        }

        private void StoreControllerSetup2()
        {
            storeControllerMock = new Mock<StoreController>(null); // sending null MembersController

            Store mockedYesStore1 = MockStoreThatReturns2(null); 
            Store mockedYesStore2 = MockStoreThatReturns2(null); 
            Store mockedNoStore = MockStoreThatReturns("Product not in store"); 

            storeControllerMock.Setup(storeController =>
                    storeController.GetOpenStore(It.Is<int>(id => id == storeYesId1))).
                        Returns(mockedYesStore1);
            storeControllerMock.Setup(storeController =>
                    storeController.GetOpenStore(It.Is<int>(id => id == storeYesId2))).
                        Returns(mockedYesStore2);
            storeControllerMock.Setup(storeController =>
                    storeController.GetOpenStore(It.Is<int>(id => id == storeNoId1))).
                        Returns(mockedNoStore);
            storeControllerMock.Setup(storeController =>
                    storeController.GetOpenStore(It.Is<int>(id => id == notAStoreId1))).
                        Returns((Store)null);




            storeController = storeControllerMock.Object;
        }

       
        private void setUpPurchase() {
            removeFromStore1FromCart = false;
            removeFromStore2FromCart = false;
            counter = 0;
            BuyersControllerSetup2();
            StoreControllerSetup2();

            purchasesManager = new PurchasesManager(storeController, buyersController, externalServicesController);
        }
        [Test]
        public void TestPurchaseFromTwoStores1Success() {
            setUpPurchase();
            Assert.IsNull(purchasesManager.PurchaseCartContent(buyerId1,case1legal));
            Assert.True(removeFromStore1FromCart && removeFromStore2FromCart);
            Assert.AreEqual(counter, 4);
        }
        [Test]
        public void TestPurchaseFromTwoStores2Success()
        {
            setUpPurchase();
            Assert.IsNull(purchasesManager.PurchaseCartContent(buyerId1, case2legal));
            Assert.True(removeFromStore1FromCart && !removeFromStore2FromCart);
            Assert.AreEqual(counter, 2);
        }
        [Test]
        public void TestPurchaseFromTwpStoresBuyerDoesNotExistFail()
        {
            setUpPurchase();
            Assert.Throws<ArgumentException>(()=>purchasesManager.PurchaseCartContent(notABuyerId1, case2legal));
            Assert.True(!removeFromStore1FromCart && !removeFromStore2FromCart);
            Assert.AreEqual(counter, 0);
        }
        [Test]
        public void TestPurchaseFromTStoresStoreDoesNotExistFail()
        {
            setUpPurchase();
            Assert.Throws<ArgumentException>(() => purchasesManager.PurchaseCartContent(notABuyerId1, case2legal));
            Assert.True(!removeFromStore1FromCart && !removeFromStore2FromCart);
            Assert.AreEqual(counter, 0);
        }
    }
}
