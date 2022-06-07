using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market;
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
    public class ProductsSearchFilterTests
    {
        private ProductsSearchFilter productsSearchFilter;

        private static Predicate<Object> returnsTrue = input => true;
        private static Predicate<Object> returnsFalse = input => false;

        private const string storeDefualtName = "theStore1"; 

        private const string productDefaultName = "productName";
        private const double productDefaultPrice = 3.3;
        private const string productDefaultCategory = "productCategory";

        // ----------- Setup helping functions -----------------------------

        [SetUp]
        public void SetupInitialProductsSearchFilter()
        {
            productsSearchFilter = new ProductsSearchFilter();
        }

        private Mock<Store> MakeStoreWithMockFields(string name = storeDefualtName)
        {
            Mock<Security> securityMock = new Mock<Security>();
            Mock<Member> founderMock = new Mock<Member>("user123", "12345678", securityMock.Object);
            Member founder = founderMock.Object; 

            return new Mock<Store>(name, founder, (int id) => (Member)null);
        }

        private Store MakeStoreMockGetName(string name)
        {
            Mock<Store> storeMcok = MakeStoreWithMockFields(name); 

            storeMcok.Setup(store =>
                    store.GetName()).
                        Returns(name);

            return storeMcok.Object;
        }

        private Store MakeStoreMockGetMembersInRole(int[] membersInRole, Role role)
        {
            Mock<Store> storeMcok = MakeStoreWithMockFields();

            IList<int> membersInRoleList; 
            foreach (Role roleToMock in Enum.GetValues(typeof(Role)))
            {
                if (roleToMock == role)
                    membersInRoleList = membersInRole.ToList();
                else
                    membersInRoleList = new List<int>();

                storeMcok.Setup(store =>
                    store.GetMembersInRoleNoPermissionsCheck(It.Is<Role>(roleArgument => roleArgument == roleToMock))).
                        Returns(membersInRoleList); 
            }

            return storeMcok.Object;
        }

        private Mock<Product> MakeProductMock(string name, double price, string category)
        {
            return new Mock<Product>(name, price, category);
        }

        private Product MakeProductMockName(string name)
        {
            Mock<Product> productMock = MakeProductMock(name, productDefaultPrice, productDefaultCategory);

            productMock.Setup(product =>
                    product.name).
                        Returns(name);

            return productMock.Object;
        }

        private Product MakeProductMockId(int id)
        {
            Mock<Product> productMock = MakeProductMock(productDefaultName, productDefaultPrice, productDefaultCategory);

            productMock.Setup(product =>
                    product.id).
                        Returns(id);

            return productMock.Object;
        }

        private Product MakeProductMockCategory(string category)
        {
            Mock<Product> productMock = MakeProductMock(productDefaultName, productDefaultPrice, category);

            productMock.Setup(product =>
                    product.category).
                        Returns(category);

            return productMock.Object;
        }

        private Product MakeProductMockNameAndCategory(string name, string category)
        {
            Mock<Product> productMock = MakeProductMock(name, productDefaultPrice, category);

            productMock.Setup(product =>
                    product.name).
                        Returns(name);

            productMock.Setup(product =>
                    product.category).
                        Returns(category);

            return productMock.Object;
        }

        // ----------- And() -----------------------------

        [Test]
        [TestCaseSource(nameof(andTestCases))]
        public void TestAnd(Predicate<Object> pred1, Predicate<Object> pred2)
        {
            Object object1 = new Object();
            Assert.AreEqual(pred1(object1) && pred2(object1), ProductsSearchFilter.And(pred1, pred2)(object1));
        }

        static Predicate<Object>[][] andTestCases =
                {
                    new Predicate<Object>[] { returnsTrue, returnsTrue },
                    new Predicate<Object>[] { returnsTrue, returnsFalse },
                    new Predicate<Object>[] { returnsFalse, returnsTrue },
                    new Predicate<Object>[] { returnsFalse, returnsFalse }
                };

        // ----------- FilterStoreName() -----------------------------

        [Test]
        [TestCase("store1", "store2", false)]
        [TestCase("store1", "store12", false)]
        [TestCase("store1", "store", true)]
        [TestCase("store1", "or", true)]
        public void TestFilterStoreName(string actualStoreName, string filterStoreName, bool expectedResult)
        {
            productsSearchFilter.FilterStoreName(filterStoreName);
            Store store = MakeStoreMockGetName(actualStoreName);
            Assert.AreEqual(expectedResult, productsSearchFilter.FilterStore(store));
        }

        // ----------- FilterStoreOfMemberInRole() -----------------------------

        [Test]
        [TestCase(new int[] { }, Role.Owner, 1, Role.Owner, false)]
        [TestCase(new int[] { 1 }, Role.Owner, 2, Role.Owner, false)]
        [TestCase(new int[] { 1 }, Role.Manager, 1, Role.Owner, false)]
        [TestCase(new int[] { 1 }, Role.Owner, 1, Role.Manager, false)]
        [TestCase(new int[] { 1 }, Role.Owner, 1, Role.Owner, true)]
        [TestCase(new int[] { 1, 2 }, Role.Owner, 2, Role.Owner, true)]
        [TestCase(new int[] { 1, 2 }, Role.Manager, 2, Role.Manager, true)]

        public void TestFilterStoreOfMemberInRole(int[] actualMembersInRole, Role actualRole, int filterMemberInRole, Role filterRole, bool expectedResult)
        {
            productsSearchFilter.FilterStoreOfMemberInRole(filterMemberInRole, filterRole); 
            Store store = MakeStoreMockGetMembersInRole(actualMembersInRole, actualRole);
            Assert.AreEqual(expectedResult, productsSearchFilter.FilterStore(store));
        }

        // ----------- FilterProductName() -----------------------------

        [Test]
        [TestCase("product1", "produc2", false)]
        [TestCase("product1", "product12", false)]
        [TestCase("product1", "product", true)]
        [TestCase("product1", "odu", true)]
        public void TestFilterProductName(string actualProductName, string filterProductName, bool expectedResult)
        {
            productsSearchFilter.FilterProductName(filterProductName);
            Product product = MakeProductMockName(actualProductName);
            Assert.AreEqual(expectedResult, productsSearchFilter.FilterProduct(product));
        }

        // ----------- FilterProductCategory() -----------------------------

        [Test]
        [TestCase("product1", "produc2", false)]
        [TestCase("product1", "product12", false)]
        [TestCase("product1", "product", true)]
        [TestCase("product1", "odu", true)]
        public void TestFilterProductCategory(string actualProductCategory, string filterProductCategory, bool expectedResult)
        {
            productsSearchFilter.FilterProductCategory(filterProductCategory);
            Product product = MakeProductMockCategory(actualProductCategory);
            Assert.AreEqual(expectedResult, productsSearchFilter.FilterProduct(product));
        }

        // ----------- FilterProductKeyword() -----------------------------

        [Test]
        [TestCase("store1", "product2", "store2", false)]
        [TestCase("store1", "product1", "product12", false)]
        [TestCase("store12", "product12", "ore1", true)]
        [TestCase("store12", "product12", "oduct1", true)]
        public void TestFilterProductKeyword(string actualProductName, string actualProductCategory, string filterProductKeyword, bool expectedResult)
        {
            productsSearchFilter.FilterProductKeyword(filterProductKeyword);
            Product product = MakeProductMockNameAndCategory(actualProductName, actualProductCategory);
            Assert.AreEqual(expectedResult, productsSearchFilter.FilterProduct(product));
        }

        // ----------- FilterProductId() -----------------------------

        [Test]
        [TestCase(1, 2, false)]
        [TestCase(1, -1, false)]
        [TestCase(1, 1, true)]
        [TestCase(3, 3, true)]
        public void TestFilterProductId(int actualProductId, int filterProductId, bool expectedResult)
        {
            productsSearchFilter.FilterProductId(filterProductId);
            Product product = MakeProductMockId(actualProductId);
            Assert.AreEqual(expectedResult, productsSearchFilter.FilterProduct(product));
        }

        // ----------- FilterProductIds() -----------------------------

        [Test]
        [TestCase(1, new int[] { 0, -1, 2 }, false)]
        [TestCase(1, new int[] { 2 }, false)]
        [TestCase(1, new int[] { 1 }, true)]
        [TestCase(3, new int[] { 1, 2, 3 }, true)]
        public void TestFilterProductIds(int actualProductId, int[] filterProductIds, bool expectedResult)
        {
            productsSearchFilter.FilterProductIds(filterProductIds.ToList());
            Product product = MakeProductMockId(actualProductId);
            Assert.AreEqual(expectedResult, productsSearchFilter.FilterProduct(product));
        }
    }
}
