using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    public class ProductTests
    {
        private Product product;

        [SetUp]
        public void setup() {
            product = new Product("Chocolate", 5.90);
        }

        // AddToInventory test
        [Test]
        [TestCase(10)]
        [TestCase(100000)]
        public void AddToInventoryCheck(int amountToAdd) {
            int amountBefore = product.amountInInventory;
            product.AddToInventory(amountToAdd);
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
            product.RemoveFromInventory(amountToRemove);
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
            Assert.Throws<MarketException>(() => product.RemoveFromInventory(amounToRemove));
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
            product.AddPurchaseOption(purchaseOptionToAdd);
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
            product.RemovePurchaseOption(purchaseOptionToRemove);
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
            Assert.Throws<MarketException>(() => product.RemovePurchaseOption(purchaseOptionToRemove));
            int amountAfter = product.purchaseOptions.Count;
            Assert.IsTrue(amountBefore == amountAfter);
        }
    }
}
