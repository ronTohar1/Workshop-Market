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
    public class StorePolicyTest
    {
        private StorePolicy storePolicy;

        private const int productId1 = 1;
        private const int productId2 = 2;
        private const int amount1 = 3;
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
        [Test]
        [TestCase(10, 65.5)]
        [TestCase(20, 70.5)]
        [TestCase(20, 90.5)]
        public void AddDiscountAmountPolicySuccess(int amount, double discountPercentage)
        {
            storePolicy.AddDiscountAmountPolicy(amount, discountPercentage);
            Assert.True(storePolicy.amountDiscount.Keys.Contains(amount) && storePolicy.amountDiscount[amount] == discountPercentage/100);
        }
        private void SetUpDiscountAmount() {
            storePolicy.AddDiscountAmountPolicy(10, 10);
            storePolicy.AddDiscountAmountPolicy(20, 25);
            storePolicy.AddDiscountAmountPolicy(30, 50);
            storePolicy.AddDiscountAmountPolicy(90, 70);
            storePolicy.AddDiscountAmountPolicy(95, 99);
        }
        [Test]
        [TestCase(13, 0.1)]
        [TestCase(21, 0.25)]
        [TestCase(30, 0.5)]
        [TestCase(92, 0.7)]
        [TestCase(10000, 0.99)]
        public void GetDiscountForAmountSuccess(int amount, double properDiscount)
        {
            SetUpDiscountAmount();
            Assert.True(storePolicy.GetDiscountForAmount(amount) == properDiscount);
        }
    }
}
