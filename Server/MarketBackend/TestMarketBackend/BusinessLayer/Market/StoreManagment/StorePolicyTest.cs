using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MarketBackend.BusinessLayer.Market.StoreManagment;
namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    public class StorePolicyTest
    {
        private StorePolicy storePolicy;

        private const int productId1 = 1;
        private const int productId2 = 2;
        private const int amount1= 3;
        private const int amount2 = 4;
        [SetUp]
        public void SetUp() {
            storePolicy = new StorePolicy();
        }
        [Test]
        [TestCase(PurchaseOption.Bid)]
        [TestCase(PurchaseOption.Public)]
        public void AddPurchaseOptionSuccess(PurchaseOption purchaseOption) {
            storePolicy.AddPurchaseOption(purchaseOption);
            Assert.True(storePolicy.ContainsPurchaseOption(purchaseOption));
        }
        private void SetUpInitialPurchaseOption()
        {
            storePolicy.AddPurchaseOption(PurchaseOption.Bid);
            storePolicy.AddPurchaseOption(PurchaseOption.Public);
        }
        [Test]
        [TestCase(PurchaseOption.Bid)]
        [TestCase(PurchaseOption.Public)]
        public void RemoveExistsPurchaseOptionSuccess(PurchaseOption purchaseOption)
        {
            SetUpInitialPurchaseOption();
            Assert.True(storePolicy.ContainsPurchaseOption(purchaseOption));
            storePolicy.RemovePurchaseOption(purchaseOption);
            Assert.False(storePolicy.ContainsPurchaseOption(purchaseOption));
        }

        [Test]
        [TestCase(PurchaseOption.Immediate)]
        [TestCase(PurchaseOption.Raffle)]
        public void RemoveDoseNotExistsPurchaseOptionFail(PurchaseOption purchaseOption)
        {
            SetUpInitialPurchaseOption();
            Assert.Throws<MarketException>(() => storePolicy.RemovePurchaseOption(purchaseOption));
            Assert.False(storePolicy.ContainsPurchaseOption(purchaseOption));
        }
        [Test]
        [TestCase(productId1, amount1)]
        [TestCase(productId2, amount2)]
        public void SetMinAmountPerProductDefine(int productId, int amount) { 
            storePolicy.SetMinAmountPerProduct(productId, amount);
            Assert.AreEqual(amount, storePolicy.GetMinAmountPerProduct(productId)); 
        }
        private void SetUpMinAmountPerProductDefineSuccess() {
            storePolicy.SetMinAmountPerProduct(productId1, amount1);
            storePolicy.SetMinAmountPerProduct(productId2, amount2);
        }
        [Test]
        [TestCase(productId1, amount1)]
        [TestCase(productId2, amount2)]
        public void SetMinAmountPerProductResetSuccess(int productId, int amount)
        {
            SetUpMinAmountPerProductDefineSuccess();
            storePolicy.SetMinAmountPerProduct(productId, amount);
            Assert.AreEqual(amount, storePolicy.GetMinAmountPerProduct(productId));
        }
    }
}
