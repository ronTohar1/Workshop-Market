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
        private bool[] removeFromStore = { false, false, false };

        private IDictionary<int, IList<Tuple<int, int>>> case1legal = new Dictionary<int, IList<Tuple<int, int>>>()
        {
            [storeYesId1] = new List<Tuple<int, int>>() { new Tuple<int, int>(productId1, amount2) }
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

            externalServicesControllerMock.Setup(externalSeviceController =>
                    externalSeviceController.makePayment()).Returns(true);
            externalServicesControllerMock.Setup(externalSeviceController =>
                    externalSeviceController.makeDelivery()).Returns(true);

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
                Assert.Throws<MarketException>(() => purchasesManager.RemoveProductFromCart(buyerId, storeId, productId, amount));
            else
                Assert.Throws<ArgumentException>(() => purchasesManager.RemoveProductFromCart(buyerId, storeId, productId, amount));
        }

        [Test]
        [TestCase(buyerId1, storeYesId1, productId1, 10)]
        [TestCase(buyerId1, storeYesId1, productId1, 130)]
        public void TestRemoveProductFromCartSholdPass(int buyerId, int storeId, int productId, int amount)
        {
            PassedValuesInitialization();
            FullPurchasesManagerSetup();
            purchasesManager.RemoveProductFromCart(buyerId, storeId, productId, amount);

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
            Mock<ShoppingBag> shoppingBagMock = new Mock<ShoppingBag>(storeYesId1, productsId.Select(index => (new Mock<ProductInBag>(index, storeYesId1)).Object).ToDictionary(p => p, P => amount1));
            
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

        private Store MockStoreThatReturns2(string result, int[] productsId)
        {
            Mock<Security> securityMock = new Mock<Security>();
            Mock<Member> founderMock = new Mock<Member>("user123", "12345678", securityMock.Object); // todo: check if okay
            Member founder = founderMock.Object;

            Mock<Store> storeMock = new Mock<Store>("store1", founder, (int id) => (Member)null);

            Mock<Product> productMock = new Mock<Product>("cheese", 5.90, "dairy");

            storeMock.Setup(store =>
                    store.DecreaseProductAmountFromInventory(It.IsAny<int>(), It.Is<int>(id => productsId.Contains(id)), It.IsAny<int>())).
                        Callback(() => counter++);

            storeMock.Setup(store =>
                    store.CanBuyProduct(It.IsAny<int>(), It.Is<int>(id => productsId.Contains(id)), It.IsAny<int>())).
                        Returns(result);
            storeMock.Setup(store =>
                   store.SearchProductByProductId(It.Is<int>(id => productsId.Contains(id)))).
                       Returns(productMock.Object);
            storeMock.Setup(store =>
                   store.AddPurchaseRecord(It.IsAny<int>(), It.IsAny<Purchase>()));
            storeMock.Setup(store =>
                   store.GetTotalBagCost(It.IsAny<IDictionary<int, int>>())).Returns(totalPrice);

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
            Assert.IsNotNull(purchasesManager.PurchaseCartContent(buyerId1));
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
            Assert.Throws<ArgumentException>(() => purchasesManager.PurchaseCartContent(notABuyerId1));
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
            Assert.Throws<ArgumentException>(() => purchasesManager.PurchaseCartContent(notABuyerId1));
            Assert.True(Array.TrueForAll(productInBagId, (index) => !removeFromStoreFromCart[index]));
            Assert.AreEqual(counter, 0);
        }
        private void setUpExternalServicesFail(int check)
        {// 0 - payment fail delivery works, 1 - payment works delivery fail , 2 both fail
            removeFromStoreFromCart = Enumerable.Repeat(false, 3).ToArray(); ;
            counter = 0;
            BuyersControllerSetup2(allValidProductId);
            StoreControllerSetup2(allValidProductId);
            externalServicesControllerMock = new Mock<ExternalServicesController>(new ExternalPaymentSystem(), new ExternalSupplySystem()); // initialized external services
            if (check == 0 || check == 2)
                externalServicesControllerMock.Setup(externalSeviceController =>
                    externalSeviceController.makePayment()).Returns(false);
            if (check == 1 || check == 2)
                externalServicesControllerMock.Setup(externalSeviceController =>
                    externalSeviceController.makeDelivery()).Returns(false);
            externalServicesController = externalServicesControllerMock.Object;
            purchasesManager = new PurchasesManager(storeController, buyersController, externalServicesController);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestPurchaseFromTStoresStoresExternalServicesFail(int check)
        {
            setUpExternalServicesFail(check);
            Assert.Throws<MarketException>(() => purchasesManager.PurchaseCartContent(buyerId1));
            Assert.True(Array.TrueForAll(removeFromStoreFromCart, (b) => !b));
            Assert.AreEqual(counter, 0);
        }
        private Store MockStoreThatCanFail(string result, int[] productsId,bool outOfStock, bool policyFail)
        {
            Mock<Security> securityMock = new Mock<Security>();
            
            Mock<Member> founderMock = new Mock<Member>("user123", "12345678", securityMock.Object) ;// todo: check if okay
            Member founder = founderMock.Object;

            Mock<Store> storeMock = new Mock<Store>("store1", founder, (int id) => (Member)null) { CallBase = true };
            Mock<Product> productMock = new Mock<Product>("cheese", 5.90, "dairy");

            Mock<StorePolicy> storePolicyMock = new Mock<StorePolicy>();

            if(outOfStock)
                productMock.Setup(product => product.amountInInventory).Returns(0);
            else
                productMock.Setup(product => product.amountInInventory).Returns(amount1);

            storeMock.Setup(store =>
                    store.DecreaseProductAmountFromInventory(It.IsAny<int>(), It.Is<int>(id => productsId.Contains(id)), It.IsAny<int>())).
                        Callback(() => counter++);
            storeMock.Setup(store =>
                   store.SearchProductByProductId(It.Is<int>(id => productsId.Contains(id)))).
                       Returns(productMock.Object);
            storeMock.Setup(store =>
                   store.AddPurchaseRecord(It.IsAny<int>(), It.IsAny<Purchase>()));
            storeMock.Setup(store =>
                   store.GetTotalBagCost(It.IsAny<IDictionary<int, int>>())).Returns(totalPrice);

            storeMock.Setup(store =>
                    store.products).
                        Returns(productsId.ToDictionary(pid => pid, pid => productMock.Object));

            if (policyFail)
                storePolicyMock.Setup(storePolicy=> storePolicy.GetMinAmountPerProduct(It.IsAny<int>())).Returns(1000);
            else
                storePolicyMock.Setup(storePolicy => storePolicy.GetMinAmountPerProduct(It.IsAny<int>())).Returns(1);

            storeMock.Setup(store =>
                    store.policy).
                        Returns(storePolicyMock.Object);

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
            Assert.Throws<MarketException>(() => purchasesManager.PurchaseCartContent(buyerId1));
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
            Assert.IsNotNull(purchasesManager.PurchaseCartContent(buyerId1));
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
            Assert.Throws<MarketException>(() => purchasesManager.PurchaseCartContent(buyerId1));
            Assert.True(Array.TrueForAll(removeFromStoreFromCart, (b) => !b));
            Assert.AreEqual(counter, 0);//check that the inventory is the same
        }
        [Test]
        [TestCase(new int[] { productId1 }, new int[] { productId1 })]
        [TestCase(new int[] { productId1 }, new int[] { productId1, productId2 })]
        [TestCase(new int[] { productId1, productId2, }, new int[] { productId1, productId2, productId3 })]
        public void TestPurchaseProductIsInCartAndMoreThanMinAmountPolicyFail(int[] cartProductsId, int[] storeProductsId)
        {
            setUpStoreServicesFail(cartProductsId, storeProductsId, false, true);
            Assert.Throws<MarketException>(() => purchasesManager.PurchaseCartContent(buyerId1));
            Assert.True(Array.TrueForAll(removeFromStoreFromCart, (b) => !b));
            Assert.AreEqual(counter, 0);//check that the inventory is the same
        }
    }
}
