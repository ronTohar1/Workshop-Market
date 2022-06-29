using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer;
using System.Threading;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    public class ProductTests
    {
        private Product product;

        [SetUp]
        public void setup() {
            DataManagersMock.InitMockDataManagers();

            product = new Product("Chocolate", 5.90, "Dairy");
        }

        // AddToInventory test
        [Test]
        [TestCase(10)]
        [TestCase(100000)]
        public void AddToInventoryCheck(int amountToAdd) {
            int amountBefore = product.amountInInventory;
            product.AddToInventory(amountToAdd, new Action(() => Thread.Sleep(0)));
            int amountAfter = product.amountInInventory;
            Assert.IsTrue(amountBefore + amountToAdd == amountAfter);
        }


        // RemoveFromInventory tests
        private void setUpInitialInventory()
            => product.amountInInventory = 10;

        [Test]
        [TestCase(5)]
        [TestCase(10)]
        public void RemoveFromInventorySuccess(int amountToRemove)
        {
            setUpInitialInventory();
            int amountBefore = product.amountInInventory;
            product.RemoveFromInventory(amountToRemove, new Action(() => Thread.Sleep(0)));
            int amountAfter = product.amountInInventory;
            Assert.IsTrue(amountBefore - amountToRemove == amountAfter);
        }

        [Test]
        [TestCase(11)]
        [TestCase(100)]
        public void RemoveFromInventoryFail (int amounToRemove)
        {
            setUpInitialInventory();
            int amountBefore = product.amountInInventory;
            Assert.Throws<MarketException>(() => product.RemoveFromInventory(amounToRemove, new Action(() => Thread.Sleep(0))));
            int amountAfter = product.amountInInventory;
            Assert.IsTrue(amountBefore == amountAfter);
        }



        // AddPurchaseOption test
        [Test]
        [TestCase(PurchaseOption.Bid)]
        [TestCase(PurchaseOption.Raffle)]
        public void AddPurchaseOptionSuccess(PurchaseOption purchaseOptionToAdd)
        {
            int amountBefore = product.purchaseOptions.Count;
            product.AddPurchaseOption(purchaseOptionToAdd, new Action(() => Thread.Sleep(0)));
            int amountAfter = product.purchaseOptions.Count;
            Assert.IsTrue(product.purchaseOptions.Contains(purchaseOptionToAdd) && amountBefore + 1 == amountAfter);
        }


        // RemovePurchaseOption tests
        private void setUpInitialPurchasesOptions()
           => product.purchaseOptions.Add(PurchaseOption.Public);
        
        [Test]
        [TestCase(PurchaseOption.Public)]
        public void RemovePurchaseOptionSuccess(PurchaseOption purchaseOptionToRemove)
        {
            setUpInitialPurchasesOptions();
            int amountBefore = product.purchaseOptions.Count;
            product.RemovePurchaseOption(purchaseOptionToRemove, new Action(() => Thread.Sleep(0)));
            int amountAfter = product.purchaseOptions.Count;
            Assert.IsTrue(!product.purchaseOptions.Contains(purchaseOptionToRemove) && amountBefore - 1 == amountAfter);
        }

        [Test]
        [TestCase(PurchaseOption.Immediate)]
        [TestCase(PurchaseOption.Raffle)]
        public void RemovePurchaseOptionFail(PurchaseOption purchaseOptionToRemove)
        {
            setUpInitialPurchasesOptions();
            int amountBefore = product.purchaseOptions.Count;
            Assert.Throws<MarketException>(() => product.RemovePurchaseOption(purchaseOptionToRemove, new Action(() => Thread.Sleep(0))));
            int amountAfter = product.purchaseOptions.Count;
            Assert.IsTrue(amountBefore == amountAfter);
        }

        // AddReview test
        [Test]
        [TestCase(1, "yummy! highly recommend!")]
        [TestCase(2, "yuck!")]
        public void AddReview(int memberId, string reviewContent)
        {
            int amountOfReviewsBefore = product.reviews.Count;
            product.AddProductReview(memberId, reviewContent, new Action(() => Thread.Sleep(0)));
            int amountOfReviewsAfter = product.reviews.Count;
            Assert.IsTrue(amountOfReviewsBefore +1 == amountOfReviewsAfter);
        }
        // SetUpPriceByUnit test
        [Test]
        [TestCase(65)]
        [TestCase(95.2)]
        public void SetPriceByUnit(double newPriceByUnit)
        {
            product.SetProductPriceByUnit(newPriceByUnit, new Action(() => Thread.Sleep(0)));
            Assert.True(product.pricePerUnit == newPriceByUnit);
        }
        // SetDiscount test
        [Test]
        [TestCase(60)]
        [TestCase(90.5)]
        public void SetDiscountPercentage(double discount)
        {
            Assert.True(product.productdicount == 0.0);
            product.SetProductDiscountPercentage(discount, new Action(() => Thread.Sleep(0)));
            Assert.True(product.productdicount == discount/100);
        }
        // SetCategory test
        [Test]
        [TestCase("Vegetables")]
        [TestCase("Fruits")]
        public void SetCategory(string category)
        {
            Assert.False(product.category.Equals(category));
            product.SetProductCategory(category, new Action(() => Thread.Sleep(0)));
            Assert.True(product.category.Equals(category));
        }
    }
}
