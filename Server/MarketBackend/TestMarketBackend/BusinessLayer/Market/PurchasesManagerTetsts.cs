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
using System.Collections.Concurrent;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy;
using System.Net.Http;
using MarketBackend.ServiceLayer.ServiceDTO;
using MarketBackend.DataLayer.DataManagers.DataManagersInherentsForTesting;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataManagers;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using System.Threading;

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
        private const int productId1 = 0;
        private const int productId2 = 1;
        private const int productId3 = 2;
        private int[] allValidProductId = new int[] { productId1, productId2, productId3 };
        private const int amount1 = 4;
        private const int amount2 = 5;
        private const int amount3 = 6;

        private const double totalPrice = 99.9;

        private int counter;
        private bool[] removeFromStoreFromCart = { false, false, false };
        private bool notifiedOwners;

        private PaymentDetails paymentDetails =
            new PaymentDetails("2222333344445555", "12", "2025", "Yossi Cohen", "262", "20444444");

        private SupplyDetails supplyDetails =
            new SupplyDetails("Yossi Cohen", "Rager 100", "Beer Sheva", "Israel", "8458527");



        // ------- Setup helping functions -------------------------------------
        [SetUp]
        public void DataManagersSetup()
        {
            // database mocks
            DataManagersMock.InitMockDataManagers(); 
        }

        private Cart MockCart()
        {
            Mock<Cart> cartMock = new Mock<Cart>();

            cartMock.Setup(cart =>
                    cart.AddProductToCart(It.IsAny<ProductInBag>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).
                        Callback(() => { addedToCart = true; });
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
            storeMock.Setup(store =>
                    store.notifyAllStoreOwners(It.IsAny<string>())).
                        Callback(() => notifiedOwners = true);
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
            HttpClient client = new HttpClient();
            externalServicesControllerMock = new Mock<ExternalServicesController>(new ExternalPaymentSystem(client), new ExternalSupplySystem(client)); // initialized external services

            externalServicesControllerMock.Setup(externalSeviceController =>
                    externalSeviceController.makePayment(paymentDetails)).Returns(1);
            externalServicesControllerMock.Setup(externalSeviceController =>
                    externalSeviceController.makeDelivery(supplyDetails)).Returns(10000);

            externalServicesController = externalServicesControllerMock.Object;
        }


        public void PassedValuesInitialization()
        {
            addedToCart = false;
            removedFromCart = false;
        }


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
            PassedValuesInitialization();
            FullPurchasesManagerSetup();
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
            PassedValuesInitialization();
            FullPurchasesManagerSetup();
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
            PassedValuesInitialization();
            FullPurchasesManagerSetup();
            if (isUserError)
                Assert.Throws<MarketException>(() => purchasesManager.RemoveProductFromCartt(buyerId, storeId, productId, amount));
            else
                Assert.Throws<ArgumentException>(() => purchasesManager.RemoveProductFromCartt(buyerId, storeId, productId, amount));
        }

        [Test]
        [TestCase(buyerId1, storeYesId1, productId1, 10)]
        [TestCase(buyerId1, storeYesId1, productId1, 130)]
        public void TestRemoveProductFromCartSholdPass(int buyerId, int storeId, int productId, int amount)
        {
            PassedValuesInitialization();
            FullPurchasesManagerSetup();
            purchasesManager.RemoveProductFromCartt(buyerId, storeId, productId, amount);

            Assert.IsTrue(removedFromCart); // cart is mocked to change this
        }


        // from here purchase managment tests

        private Cart MockCart2(int[] productsId)
        {
            Mock<Cart> cartMock = new Mock<Cart>();
            Mock<ProductInBag> productInBagMock = new Mock<ProductInBag>();
            cartMock.Setup(cart =>
                    cart.RemoveProductFromCart(It.Is<ProductInBag>(p => p != null && productsId.Contains(p.ProductId)))).
                        Callback<ProductInBag>((p) => removeFromStoreFromCart[p.ProductId] = true);
            
            Dictionary<ProductInBag, int> productInBag = new Dictionary<ProductInBag, int>();
            
            foreach (int index in productsId) {
                Mock<ProductInBag> productInBMock = new Mock<ProductInBag>(index, storeYesId1) { CallBase = true };
                productInBag.Add(productInBMock.Object, amount1);
            }
            
            Mock<ShoppingBag> shoppingBagMock = new Mock<ShoppingBag>(storeYesId1, productInBag) { CallBase = true };
            
            shoppingBagMock.Setup(shoppingBag => shoppingBag.StoreId).Returns(storeYesId1);
            cartMock.Setup(cart =>
                    cart.ShoppingBags.Values).Returns(new List<ShoppingBag> { shoppingBagMock.Object });

            cartMock.Setup(cart =>
                    cart.isEmpty()).
                        Returns(false);

            return cartMock.Object;
        }
        private Buyer MockBuyer2(int[] cartId)
        {
            Mock<Buyer> buyerMock = new Mock<Buyer>();

            Cart cart = MockCart2(cartId);

            buyerMock.Setup(buyer =>
                    buyer.Cart).
                        Returns(cart);
            buyerMock.Setup(buyer =>
                    buyer.AddPurchase(It.IsAny<Purchase>()));
            return buyerMock.Object;
        }
        private void BuyersControllerSetup2(int[] cartId)
        {
            buyersControllerMock = new Mock<BuyersController>();

            Buyer mockedBuyer = MockBuyer2(cartId);

            buyersControllerMock.Setup(buyersController =>
                    buyersController.GetBuyer(It.Is<int>(id => id == buyerId1))).
                        Returns(mockedBuyer);
            buyersControllerMock.Setup(buyersController =>
                    buyersController.GetBuyer(It.Is<int>(id => id == notABuyerId1))).
                        Returns((Buyer)null);

            buyersController = buyersControllerMock.Object;
        }

        private IDictionary<int, Product> createMocksProducts(int[] productsId, bool outOfStock)
        {
            IDictionary<int, Product> idsToProducts = new ConcurrentDictionary<int, Product>();

            foreach (int idx in productsId)
            {
                Mock<Product> productMock = new Mock<Product>("cheese", 5.90, "dairy");
                if (outOfStock)
                    productMock.Setup(product => product.amountInInventory).Returns(0);
                else
                    productMock.Setup(product => product.amountInInventory).Returns(amount1);
                productMock.Setup(product => product.id).Returns(idx);
                productMock.Setup(product => product.AddToInventory(It.IsAny<int>(), new Action(() => Thread.Sleep(0)))).Callback(()=>counter--);
                productMock.Setup(product => product.RemoveFromInventory(It.IsAny<int>(), new Action(() => Thread.Sleep(0)))).Callback(() => counter++);
                
                idsToProducts[idx] = productMock.Object;
            }
            return idsToProducts;
        }

        private Store MockStoreThatReturns2(string result, int[] productsId)
        {
            Mock<Security> securityMock = new Mock<Security>();
            Mock<Member> founderMock = new Mock<Member>("user123", "12345678", securityMock.Object); // todo: check if okay
            Member founder = founderMock.Object;

            Mock<Store> storeMock = new Mock<Store>("store1", founder, (int id) => (Member)null) { CallBase = true };

           


            storeMock.Setup(store =>
                    store.CanBuyProduct(It.IsAny<int>(), It.Is<int>(id => productsId.Contains(id)), It.IsAny<int>())).
                        Returns(result);
            storeMock.Setup(store =>
                    store.products).
                        Returns(createMocksProducts(productsId, false));
            storeMock.Setup(store =>
                   store.AddPurchaseRecord(It.IsAny<int>(), It.IsAny<Purchase>()));
            storeMock.Setup(store =>
                   store.GetTotalBagCost(It.IsAny<ShoppingBag>())).Returns(new Tuple<double,double>(totalPrice,0));
            storeMock.Setup(store =>
                    store.notifyAllStoreOwners(It.IsAny<string>())).
                        Callback(() => notifiedOwners = true);
            return storeMock.Object;
        }

        private void StoreControllerSetup2(int[] productsId)
        {
            storeControllerMock = new Mock<StoreController>(null); // sending null MembersController

            Store mockedYesStore1 = MockStoreThatReturns2(null, productsId);
            Store mockedYesStore2 = MockStoreThatReturns2(null, productsId);
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


        private void setUpPurchase(int[] productsId)
        {
            removeFromStoreFromCart = Enumerable.Repeat(false, 3).ToArray(); ;
            counter = 0;
            BuyersControllerSetup2(productsId);
            StoreControllerSetup2(productsId);
            ExternalServicesControllerSetup();
            purchasesManager = new PurchasesManager(storeController, buyersController, externalServicesController);
        }
        [Test]
        [TestCase(new int[] { productId1 })]
        [TestCase(new int[] { productId1, productId2 })]
        [TestCase(new int[] { productId1, productId2, productId3 })]
        public void TestPurchaseFromTwoStoresSuccess(int[] productInBagId)
        {
            setUpPurchase(productInBagId);
            Assert.IsNotNull(purchasesManager.PurchaseCartContent(buyerId1, paymentDetails, supplyDetails));
            Assert.True(Array.TrueForAll(productInBagId, (index) => removeFromStoreFromCart[index]));
            Assert.AreEqual(counter, productInBagId.Length);
        }
        [Test]
        [TestCase(new int[] { productId1 })]
        [TestCase(new int[] { productId1, productId2 })]
        [TestCase(new int[] { productId1, productId2, productId3 })]
        public void TestPurchaseFromTwpStoresBuyerDoesNotExistFail(int[] productInBagId)
        {
            setUpPurchase(productInBagId);
            Assert.Throws<ArgumentException>(() => purchasesManager.PurchaseCartContent(notABuyerId1, paymentDetails, supplyDetails));
            Assert.True(Array.TrueForAll(productInBagId, (index) => !removeFromStoreFromCart[index]));
            Assert.AreEqual(counter, 0);
        }
        [Test]
        [TestCase(new int[] { productId1 })]
        [TestCase(new int[] { productId1, productId2 })]
        [TestCase(new int[] { productId1, productId2, productId3 })]
        public void TestPurchaseFromTStoresStoreDoesNotExistFail(int[] productInBagId)
        {
            setUpPurchase(productInBagId);
            Assert.Throws<ArgumentException>(() => purchasesManager.PurchaseCartContent(notABuyerId1, paymentDetails, supplyDetails));
            Assert.True(Array.TrueForAll(productInBagId, (index) => !removeFromStoreFromCart[index]));
            Assert.AreEqual(counter, 0);
        }
        private void setUpExternalServicesFail(int check)
        {
            removeFromStoreFromCart = Enumerable.Repeat(false, 3).ToArray(); ;
            counter = 0;
            BuyersControllerSetup2(allValidProductId);
            StoreControllerSetup2(allValidProductId);

            HttpClient client = new HttpClient();
            externalServicesControllerMock = new Mock<ExternalServicesController>(new ExternalPaymentSystem(client), new ExternalSupplySystem(client)); // initialized external services
            if (check == 0 || check == 2)
                externalServicesControllerMock.Setup(externalSeviceController =>
                    externalSeviceController.makePayment(paymentDetails)).Returns(-1);
            if (check == 1 || check == 2)
                externalServicesControllerMock.Setup(externalSeviceController =>
                    externalSeviceController.makeDelivery(supplyDetails)).Returns(-1);
            externalServicesController = externalServicesControllerMock.Object;
            purchasesManager = new PurchasesManager(storeController, buyersController, externalServicesController);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]// 0 - payment fail delivery works, 1 - payment works delivery fail , 2 both fail
        public void TestPurchaseFromTStoresStoresExternalServicesFail(int check)
        {
            setUpExternalServicesFail(check);
            Assert.Throws<MarketException>(() => purchasesManager.PurchaseCartContent(buyerId1, paymentDetails, supplyDetails));
            Assert.True(Array.TrueForAll(removeFromStoreFromCart, (b) => !b));
            Assert.AreEqual(counter, 0);
        }
      

        //TODO
        private Store MockStoreThatCanFail(string result, int[] productsId,bool outOfStock, bool policyFail)
        {
            Mock<Security> securityMock = new Mock<Security>();
            
            Mock<Member> founderMock = new Mock<Member>("user123", "12345678", securityMock.Object) ;// todo: check if okay
            Member founder = founderMock.Object;

            Mock<Store> storeMock = new Mock<Store>("store1", founder, (int id) => (Member)null) { CallBase = true };
          
            
            storeMock.Setup(store =>
                    store.DecreaseProductAmountFromInventory(It.IsAny<int>(), It.Is<int>(id => productsId.Contains(id)), It.IsAny<int>())).
                        Callback(() => counter++);
            storeMock.Setup(store =>
                   store.AddProductToInventory(It.IsAny<int>(), It.Is<int>(id => productsId.Contains(id)), It.IsAny<int>())).
                       Callback(() => counter++);

            storeMock.Setup(store =>
                   store.AddPurchaseRecord(It.IsAny<int>(), It.IsAny<Purchase>()));
            storeMock.Setup(store =>
                   store.GetTotalBagCost(It.IsAny<ShoppingBag>())).Returns(new Tuple<double, double>(totalPrice,0)); // TODO

            storeMock.Setup(store =>
                    store.products).
                        Returns(createMocksProducts(productsId, outOfStock));

            storeMock.Setup(store =>
                    store.notifyAllStoreOwners(It.IsAny<string>())).
                        Callback(()=>notifiedOwners=true);

            Mock<StorePurchasePolicyManager> storePurchasePolicyManagerMock = new Mock<StorePurchasePolicyManager>();
            if (!policyFail)
                storePurchasePolicyManagerMock.Setup(storePurchasePolicy => storePurchasePolicy.CanBuy(It.IsAny<ShoppingBag>(), It.IsAny<string>())).Returns((string?)null);
            else
                storePurchasePolicyManagerMock.Setup(storePurchasePolicy => storePurchasePolicy.CanBuy(It.IsAny<ShoppingBag>(), It.IsAny<string>())).Returns("can't buy ):");
            storeMock.Setup(store =>
                   store.purchaseManager).
                       Returns(storePurchasePolicyManagerMock.Object);

            return storeMock.Object;
        }

        private void StoreControllerSetupStoreCouldFail(int[] productsId, bool outOfStock, bool policyFail)
        {
            storeControllerMock = new Mock<StoreController>(null); // sending null MembersController

            Store mockedYesStore1 = MockStoreThatCanFail(null, productsId, outOfStock, policyFail);
    
            storeControllerMock.Setup(storeController =>
                    storeController.GetOpenStore(It.Is<int>(id => id == storeYesId1))).
                        Returns(mockedYesStore1);
        

            storeController = storeControllerMock.Object;
        }
        private void setUpStoreServicesFail(int[] cartProductsId, int[] storeProductsId, bool outOfStock, bool policyFail) {
            notifiedOwners = false;
            removeFromStoreFromCart = Enumerable.Repeat(false, 3).ToArray();
            counter = 0;
            BuyersControllerSetup2(cartProductsId);
            StoreControllerSetupStoreCouldFail(storeProductsId, outOfStock, policyFail);
            ExternalServicesControllerSetup();
            purchasesManager = new PurchasesManager(storeController, buyersController, externalServicesController);
        }
        [Test]
        [TestCase(new int[] { productId1 }, new int[] { productId2 })]
        [TestCase(new int[] { productId1 }, new int[] { productId2, productId3 })]
        [TestCase(new int[] { productId1, productId2 }, new int[] { productId2 })]
        [TestCase(new int[] { productId2, productId3 }, new int[] { productId1 })]
        public void TestPurchaseProductIsInCartIsntInStoreFail(int[] cartProductsId, int[] storeProductsId) {
            setUpStoreServicesFail(cartProductsId, storeProductsId, false, false);
            Assert.Throws<Exception>(() => purchasesManager.PurchaseCartContent(buyerId1, paymentDetails, supplyDetails));
            Assert.True(Array.TrueForAll(removeFromStoreFromCart, (b) => !b));
            Assert.AreEqual(counter, 0);//check that the inventory is the same
        }

        [Test]
        [TestCase(new int[] { productId1 }, new int[] { productId1 })]
        [TestCase(new int[] { productId1 }, new int[] { productId1, productId2 })]
        [TestCase(new int[] { productId1, productId2, }, new int[] { productId1, productId2, productId3 })]
        public void TestPurchaseProductIsInCartAndStoreGreatSuccess(int[] cartProductsId, int[] storeProductsId)
        {
            setUpStoreServicesFail(cartProductsId, storeProductsId, false, false);
            Assert.IsNotNull(purchasesManager.PurchaseCartContent(buyerId1, paymentDetails, supplyDetails));
            Assert.True(Array.TrueForAll(cartProductsId, (index) => removeFromStoreFromCart[index]));
            Assert.AreEqual(counter, cartProductsId.Length);//check that the inventory is the same
        }

        [Test]
        [TestCase(new int[] { productId1 }, new int[] { productId1 })]
        [TestCase(new int[] { productId1 }, new int[] { productId1, productId2})]
        [TestCase(new int[] { productId1, productId2, }, new int[] { productId1, productId2, productId3 })]
        public void TestPurchaseProductIsInCartProductOutOfStockFail(int[] cartProductsId, int[] storeProductsId)
        {
            setUpStoreServicesFail(cartProductsId, storeProductsId, true, false);
            Assert.Throws<MarketException>(() => purchasesManager.PurchaseCartContent(buyerId1, paymentDetails, supplyDetails));
            Assert.True(Array.TrueForAll(removeFromStoreFromCart, (b) => !b));
            Assert.AreEqual(counter, 0);//check that the inventory is the same
        }
        [Test]
        [TestCase(new int[] { productId1 }, new int[] { productId1 })]
        [TestCase(new int[] { productId1 }, new int[] { productId1, productId2 })]
        [TestCase(new int[] { productId1, productId2, }, new int[] { productId1, productId2, productId3 })]
        public void TestPurchaseProductIsInCartAndPolicyFail(int[] cartProductsId, int[] storeProductsId)
        {
            setUpStoreServicesFail(cartProductsId, storeProductsId, false, true);
            Assert.Throws<MarketException>(() => purchasesManager.PurchaseCartContent(buyerId1, paymentDetails, supplyDetails));
            Assert.True(Array.TrueForAll(removeFromStoreFromCart, (b) => !b));
            Assert.AreEqual(counter, 0);//check that the inventory is the same
            Assert.False(notifiedOwners);
        }
    }
}
