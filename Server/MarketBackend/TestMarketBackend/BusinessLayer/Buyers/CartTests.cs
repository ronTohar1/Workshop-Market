using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.Buyers;
using Autofac.Extras.Moq;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    public class CartTests
    {
        private ShoppingBag bag1 = new ShoppingBag();
        private ShoppingBag bag2 = new ShoppingBag();
        private ProductInBag product = new ProductInBag(1,1);

        private void SetupShoppingBags(Cart cart)
        {
            cart.ShoppingBags[1] = bag1;
            cart.ShoppingBags[2] = bag2;
        }
        
        [Test]
        //[TestCase(coOwnerId1, memberId1)]
        public void TestAddProductToCart_Pass()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ShoppingBag>()
                    .Setup(bag => bag.AddProductToBag(product, 5))
                    .Callback(() => Assert.Pass());

                ShoppingBag bag = mock.Create<ShoppingBag>();
                Cart cart = mock.Create<Cart>();
                cart.ShoppingBags.Add(1, bag);
                cart.AddProductToCart(product, 5);
                Assert.Fail();
            }
        }
    }
}