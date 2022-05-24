using MarketBackend.ServiceLayer.ServiceDTO;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO;
using MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs;

namespace TestMarketBackend.Acceptance
{
    internal class StoreOwner_ManagerReqsTests : AcceptanceTests
    {

        public static IEnumerable<TestCaseData> DataSuccessfulProductAdditionToStore
        {
            get
            {
                yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), "Gaming Chair", 800, "Gaming", 20);
            }
        }

        // r.4.1
        [Test]
        [TestCaseSource("DataSuccessfulProductAdditionToStore")]
        public void SuccessfulProductAdditionToStore(
           Func<int> userId1, Func<int> storeId1, string productName, double price, string category, int amount)
        {
            int userId = userId1();
            int storeId = storeId1();
            // Adding the product
            Response<int> productIdResponse =
                storeManagementFacade.AddNewProduct(userId, storeId, productName, price, category);

            Assert.IsTrue(!productIdResponse.ErrorOccured());

            int productId = productIdResponse.Value;

            Response<bool> response =
                storeManagementFacade.AddProductToInventory(userId, storeId, productId, amount);

            Assert.IsTrue(!response.ErrorOccured());

            // Checking that it is available
            response = buyerFacade.AddProdcutToCart(userId, storeId, productId, 5);

            Assert.IsTrue(!response.ErrorOccured());
        }

        /*TODO
        // r.4.1
        [Test]
        public void SuccessfulProductDetailsUpdate()
        {

        }

        // r.4.1
        [Test]
        public void SuccessfulProductRemovalFromStore()
        {

        }
        */

        public static IEnumerable<TestCaseData> DataSuccessfulProductDecreaseInStore
        {
            get
            {
                yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), new Func<int>(() => iphoneProductId), new Func<int>(() => iphoneProductAmount / 2));
                yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), new Func<int>(() => calculatorProductId), new Func<int>(() => calculatorProductAmount / 2));
            }
        }

        // r.4.1
        [Test]
        [TestCaseSource("DataSuccessfulProductDecreaseInStore")]
        public void SuccessfulProductDecreaseInStore(Func<int> ownerId, Func<int> storeId, Func<int> productId, Func<int> amount)
        {
            Response<bool> response = storeManagementFacade.DecreaseProduct(ownerId(), storeId(), productId(), amount());

            Assert.IsTrue(!response.ErrorOccured());
        }

        public static IEnumerable<TestCaseData> DataFailedInvalidProductAdditionToStore
        {
            get
            {
                //yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), "124@#$!@$4444", 800, "Gaming");
                //yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), "Gaming Chair", 800, "!@#$eddddddd");
                yield return new TestCaseData(new Func<int>(() => storeOwnerId), new Func<int>(() => storeId), "Gaming Chair", -400, "Gaming");
            }
        }

        // r.4.1
        [Test]
        [TestCaseSource("DataFailedInvalidProductAdditionToStore")]
        public void FailedInvalidProductAdditionToStore(
            Func<int> userId1, Func<int> storeId1, string productName, double price, string category)
        {
            int userId = userId1();
            int storeId = storeId1();
            // Adding the product
            Response<int> productIdResponse =
                storeManagementFacade.AddNewProduct(userId, storeId, productName, price, category);

            Assert.IsTrue(productIdResponse.ErrorOccured());
        }

        public static IEnumerable<TestCaseData> DataFailedProductDecrease
        {
            get
            {
                yield return new TestCaseData(storeOwnerId, storeId, iphoneProductId, 99999);
                yield return new TestCaseData(storeOwnerId, storeId, 123, 1);
            }
        }

        // r.4.1
        [Test]
        [TestCaseSource("DataFailedProductDecrease")]
        public void FailedProductDecrease(int userId, int storeId, int productId, int amount)
        {
            Response<bool> response =
                storeManagementFacade.DecreaseProduct(userId, storeId, productId, amount);

            Assert.IsTrue(response.ErrorOccured());
        }

        private bool MemberIsRoleInStore(int storeOwnerId, int memberId, int storeId, Role role)
        {
            Response<IList<int>> ownersResponse =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, role);

            Assert.IsTrue(!ownersResponse.ErrorOccured());

            IList<int> ownersIds = ownersResponse.Value;
            return ownersIds.Contains(memberId);
        }

        // r.4.4
        [Test]
        public void SuccessfulStoreOwnerAppointment()
        {
            Response<bool> response = storeManagementFacade.MakeCoOwner(storeOwnerId, member1Id, storeId);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsTrue(MemberIsRoleInStore(storeOwnerId, member1Id, storeId, Role.Owner));
        }

        // r.4.4
        [Test]
        public void FailedStoreOwnerAppointment()
        {
            Response<IList<int>> ownersBefore =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Owner);

            // Appointing a store owner as a store owner
            Response<bool> response = storeManagementFacade.MakeCoOwner(storeOwnerId, member3Id, storeId);

            Response<IList<int>> ownersAfter =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Owner);

            Assert.IsTrue(response.ErrorOccured());
            Assert.IsTrue(SameElements(ownersBefore.Value, ownersAfter.Value));
        }

        // r 4.4
        // r S 5
        [Test]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(50)]
        public void ConcurrentStoreOwnerAppointment(int threadsNumber)
        {
            // todo: maybe add more test cases on other things such as different members, different stores etc. 
            Response<IList<int>> ownersBefore =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Owner);

            // Appointing the same member by many threads 
            Func<Response<bool>>[] jobs =
                Enumerable.Repeat(() => storeManagementFacade.MakeCoOwner(storeOwnerId, member1Id, storeId), threadsNumber).ToArray();

            Response<bool>[] responses = GetResponsesFromThreads(jobs);

            Assert.IsTrue(Exactly1ResponseIsSuccessful(responses));

            Response<IList<int>> ownersAfter =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Owner);

            IList<int> expectedOwnersAfter = ownersBefore.Value;
            expectedOwnersAfter.Add(member1Id);

            Assert.IsTrue(SameElements(expectedOwnersAfter, ownersAfter.Value));
        }

        // succesful, need to check:

        //      the store owner removed is removed
        //      the store owners it appointed are removed 
        //      the store managers it appointed are removed

        // concurrent tests for removing the store owner 

        public static IEnumerable<TestCaseData> DataFailedRemoveCoOwnerAppointment
        {
            get
            {
                // remove not a buyer
                yield return new TestCaseData(() => member2Id, () => storeId, () => -1);
                // remove a guest
                yield return new TestCaseData(() => member2Id, () => storeId, () => guest1Id);
                // remove a member
                yield return new TestCaseData(() => member2Id, () => storeId, () => member1Id);
                // remove a manager (appointed by the requesting  co owner) 
                yield return new TestCaseData(() => member2Id, () => storeId, () => member4Id);
                // remove a store owner that it not appointed by the requsting co owner 
                yield return new TestCaseData(() => member3Id, () => storeId, () => member2Id);
                // not a storeId
                // nice to have to do sometime maybe, not doing this at the moment so we can check the roles in the store before and after the test
                // by not a buyer
                yield return new TestCaseData(() => -1, () => storeId, () => member2Id);
                // by a guest
                yield return new TestCaseData(() => guest1Id, () => storeId, () => member2Id);
                // by a member (that is not a co owner or manager in the store)
                yield return new TestCaseData(() => member1Id, () => storeId, () => member2Id);
                // by a manager
                yield return new TestCaseData(() => member4Id, () => storeId, () => member2Id);
            }
        }

        // r 4.5, r I 5
        [Test]
        [TestCaseSource("DataFailedRemoveCoOwnerAppointment")]
        public void FailedRemoveCoOwnerAppointment(Func<int> requestingId, Func<int> storeId, Func<int> coOwnerToRemoveId)
        {
            // getting roles before to check the roles after the action 
            IDictionary<Role, IList<int>> rolesBefore = GetRolesInStore(storeId());

            // in some test cases the action is removing member2, checking that in this case member2 does not receive new notifications
            IList<string> notificationsBefore = null;
            if (coOwnerToRemoveId() == member2Id)
            {
                notificationsBefore = notificationsBefore = member2Notifications.ToList();
            }

            Response<bool> response = storeManagementFacade.RemoveCoOwner(requestingId(), coOwnerToRemoveId(), storeId());

            Assert.IsTrue(response.ErrorOccured());

            // check that roles in the store remained as before 
            IDictionary<Role, IList<int>> roles = GetRolesInStore(storeId());

            Assert.IsTrue(SameDictionariesWithLists(rolesBefore, roles));

            if (notificationsBefore != null)
            {
                Assert.IsTrue(SameElements(notificationsBefore, member2Notifications.ToList()));
            }
        }

        // r 4.5
        [Test]
        public void SuccessfulRemoveCoOwnerAppointment()
        {
            int requestingId = member2Id;
            // using storeId
            int coOwnerToRemoveId = member5Id; // appointed by 2, and appointed 6 and 7

            // getting roles before to check the roles after the action 
            IDictionary<Role, IList<int>> expectedRoles = GetRolesInStore(storeId);
            expectedRoles[Role.Owner].Remove(coOwnerToRemoveId);
            expectedRoles[Role.Owner].Remove(member6Id);
            expectedRoles[Role.Manager].Remove(member7Id);

            Response<bool> response = storeManagementFacade.RemoveCoOwner(requestingId, coOwnerToRemoveId, storeId);
            Assert.IsTrue(!response.ErrorOccured());

            // check that roles in the store where changed as needed 
            IDictionary<Role, IList<int>> roles = GetRolesInStore(storeId);

            Assert.IsTrue(SameDictionariesWithLists(expectedRoles, roles));
        }

        // r 4.5
        // r S 5
        [Test]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(50)]
        public void ConcurrentRemoveCoOwnerAppointment(int threadsNumber)
        {
            int requestingId = member2Id;
            // using storeId
            int coOwnerToRemoveId = member5Id; // appointed by 2, and appointed 6 and 7

            // getting roles before to check the roles after the action 
            IDictionary<Role, IList<int>> expectedRoles = GetRolesInStore(storeId);
            expectedRoles[Role.Owner].Remove(coOwnerToRemoveId);
            expectedRoles[Role.Owner].Remove(member6Id);
            expectedRoles[Role.Manager].Remove(member7Id);

            Func<Response<bool>>[] jobs =
                Enumerable.Repeat(() => storeManagementFacade.RemoveCoOwner(requestingId, coOwnerToRemoveId, storeId), threadsNumber).ToArray();
            Response<bool>[] responses = GetResponsesFromThreads(jobs);
            Assert.IsTrue(Exactly1ResponseIsSuccessful(responses));

            // check that roles in the store where changed as needed 
            IDictionary<Role, IList<int>> roles = GetRolesInStore(storeId);

            Assert.IsTrue(SameDictionariesWithLists(expectedRoles, roles));
        }

        // r.4.6
        [Test]
        public void SuccessfulStoreManagerAppointment()
        {
            Response<bool> response = storeManagementFacade.MakeCoManager(storeOwnerId, member1Id, storeId);

            Assert.IsTrue(!response.ErrorOccured());
            Assert.IsTrue(MemberIsRoleInStore(storeOwnerId, member1Id, storeId, Role.Manager));
        }

        // r.4.6
        [Test]
        public void FailedStoreManagerAppointment()
        {
            Response<IList<int>> managersBefore =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Manager);

            // Appointing a store manager as a store manager
            Response<bool> response = storeManagementFacade.MakeCoManager(storeOwnerId, member4Id, storeId);

            Response<IList<int>> managersAfter =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Manager);

            Assert.IsTrue(response.ErrorOccured());
            Assert.IsTrue(SameElements(managersBefore.Value, managersAfter.Value));
        }

        public static IEnumerable<TestCaseData> DataSuccessfulStoreManagerPermissionsAddition
        {
            get
            {
                yield return new TestCaseData(new List<Permission>()
                    { Permission.RecieveInfo, Permission.RecieiveRolesInfo });
                yield return new TestCaseData(new List<Permission>()
                    { Permission.RecieiveRolesInfo });
                yield return new TestCaseData(new List<Permission>()
                    { Permission.MakeCoOwner });
                yield return new TestCaseData(new List<Permission>()
                    { Permission.RemoveCoManager });
            }
        }

        // r.4.7
        [Test]
        [TestCaseSource("DataSuccessfulStoreManagerPermissionsAddition")]
        public void SuccessfulStoreManagerPermissionsAddition(IList<Permission> permissions)
        {
            Response<bool> response =
                storeManagementFacade.ChangeManagerPermission(storeOwnerId, member4Id, storeId, permissions);

            Response<IList<Permission>> newPermissionResponse =
                storeManagementFacade.GetManagerPermissions(storeId, storeOwnerId, member4Id);

            Assert.IsTrue(!response.ErrorOccured() && !newPermissionResponse.ErrorOccured());
            Assert.IsTrue(SameElements(newPermissionResponse.Value, permissions));
        }
        /*TODO
        // r.4.9
        public void SuccessfulStoreClosing()
        {
            Response<bool> response = storeManagementFacade.CloseStore(storeOwnerId, storeId);

            Assert.IsTrue(!response.ErrorOccured());

            // After closing, only owners and system managers can get any information about the store
            response = storeManagementFacade.GetPurchaseHistory(member4Id, storeId);

            Assert.IsTrue(response.ErrorOccured());
        }

        // r.4.9
        public void FailedClosedStoreClosing()
        {
            // todo: add check that the user did not receive notifications for the failed action 
            Response<bool> response = storeManagementFacade.CloseStore(storeOwnerId, storeId);

            Assert.IsTrue(!response.ErrorOccured());

            // closing an already closed store
            response = storeManagementFacade.CloseStore(storeOwnerId, storeId);

            Assert.IsTrue(response.ErrorOccured());
        }
        */
        // r.4.11

        [Test]
        public void SuccessfulGetStorePersonnelInformation()
        {
            Response<IList<int>> managers =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Manager);

            Assert.IsTrue(!managers.ErrorOccured());
            Assert.IsTrue(SameElements(managers.Value, new List<int>() { member4Id, member7Id }));

            Response<IList<int>> owners =
                storeManagementFacade.GetMembersInRole(storeId, storeOwnerId, Role.Owner);

            Assert.IsTrue(!owners.ErrorOccured());
            Assert.IsTrue(SameElements(owners.Value, new List<int>() { storeOwnerId, member3Id, member5Id, member6Id }));
        }

        [Test]
        // r.4.13
        public void SuccessfulGetStorePurchaseHistoryInformation()
        {
            Response<IList<Purchase>> purchaseHistory =
                storeManagementFacade.GetPurchaseHistory(storeOwnerId, storeId);

            Assert.IsTrue(!purchaseHistory.ErrorOccured());
        }

        private IDictionary<Role, IList<int>> GetRolesInStore(int storeId)
        {
            IDictionary<Role, IList<int>> roles = new Dictionary<Role, IList<int>>();
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                Response<IList<int>> membersInRoleResponse = storeManagementFacade.GetMembersInRole(storeId, member2Id, role); // notice member2 is co owner in all stores 
                Assert.IsTrue(!membersInRoleResponse.ErrorOccured());
                roles[role] = membersInRoleResponse.Value;
            }
            return roles;
        }

        // discounts and byuing policies related tests 

        // cc 6.1, cc 6.2 - a store must have discounts and buying policies 
        // r 2.5 - buying according to discounts etc. 
        // r 4.2 - changing discounts and buying policies 

        public static IEnumerable<TestCaseData> DataFailedAddDiscount
        {
            get
            {
                string description = "two iphones discount";
                int discountPrecentage = 10;
                // memberId is -1
                yield return new TestCaseData(
                    () => new ServiceProductDiscount(iphoneProductId, discountPrecentage),
                    description, () => storeId, () => -1);
                // guest id
                yield return new TestCaseData(
                    () => new ServiceProductDiscount(iphoneProductId, discountPrecentage),
                    description, () => storeId, () => guest1Id);
                // member id (does not have role in store) 
                yield return new TestCaseData(
                    () => new ServiceProductDiscount(iphoneProductId, discountPrecentage),
                    description, () => storeId, () => member1Id);
                // manager id no permission
                yield return new TestCaseData(
                    () => new ServiceProductDiscount(iphoneProductId, discountPrecentage),
                    description, () => storeId, () => member4Id); // not in defalut permissions 
                // coOwner in different store
                yield return new TestCaseData(
                    () => new ServiceProductDiscount(iphoneProductId, discountPrecentage),
                    description, () => store2Id, () => member5Id);

                // storeId is -1
                yield return new TestCaseData(
                    () => new ServiceProductDiscount(iphoneProductId, discountPrecentage),
                    description, () => -1, () => member3Id);

                // currently can add a discount to a product that is not currently in the store 
            }
        }

        [Test]
        [TestCaseSource("DataFailedAddDiscount")]
        public void FailedAddDiscount(Func<IServiceExpression> discountExpression, string description, Func<int> storeId, Func<int> memberId)
        {
            Response<int> response = storeManagementFacade.AddDiscountPolicy(discountExpression(), description, storeId(), memberId());
            Assert.IsTrue(response.ErrorOccured());

            // checking that discount description is not in discounts 

            Response<IDictionary<int, string>> descriptionsResposne = buyerFacade.GetDiscountsDescriptions(storeId());
            if (!descriptionsResposne.ErrorOccured()) // the arguments can make an expected error here, for example storeId does not exists 
            {
                Assert.IsTrue(!descriptionsResposne.Value.Values.Contains(description));
            }

        }

        // helping classes for adding discounts tests 

        private class AddProductToCartArguments
        {
            public Func<int> ProductId { get; set; }
            public Func<int> Amount { get; set; }
        }

        private class TestAddDiscountProductArguments
        {
            public Func<int> ProductId { get; set; }
            public Func<int> Amount { get; set; }
            public Func<int> Price { get; set; }
            public Func<int> Discount { get; set; } = () => 0; // default 
        }

        // helping functions for adding discount tests 

        private static Func<StoreOwner_ManagerReqsTests, ServicePurchase> GetPurchaseProcess(IList<AddProductToCartArguments> addingsToCart, bool shouldSucceedBuying = true)
        {
            return (StoreOwner_ManagerReqsTests thisObject) =>
            {
                Response<bool> response;
                foreach (AddProductToCartArguments addingToCart in addingsToCart)
                {
                    response = thisObject.buyerFacade.AddProdcutToCart(member1Id, storeId, addingToCart.ProductId(), addingToCart.Amount());
                    Assert.IsTrue(!response.ErrorOccured());
                }

                Response<ServicePurchase> purchaseReponse = thisObject.buyerFacade.PurchaseCartContent(member1Id);
                Assert.AreEqual(shouldSucceedBuying, !purchaseReponse.ErrorOccured());
                return purchaseReponse.Value;
            };
        }

        private static TestCaseData AddDiscountTestCase(Func<IServiceExpression> getDiscount, IList<TestAddDiscountProductArguments> arguments, string description, int generalDiscount = 0)
        {
            return new TestCaseData(

                    // discout
                    getDiscount, description,

                    // store and requesting (to add) member 
                    () => storeId, () => member3Id,
                    // adding to cart
                    GetPurchaseProcess(
                        arguments.Select(argumentObject =>
                            new AddProductToCartArguments()
                            {
                                ProductId = argumentObject.ProductId,
                                Amount = argumentObject.Amount
                            }).ToList()
                    ),

                    // expected price
                    (100 - generalDiscount) / 100.0 * 
                    arguments.Aggregate(0.0, (price, argumentObject) =>
                        price + 
                            argumentObject.Amount() * 
                            argumentObject.Price() * 
                            (1 - argumentObject.Discount() / 100.0))
            );
        }

        private static TestAddDiscountProductArguments GetIphoneArgument(int amount, int discount = 0)
        {
            return new TestAddDiscountProductArguments()
            {
                ProductId = () => iphoneProductId,
                Price = () => iphoneProductPrice,
                Amount = () => amount,
                Discount = () => discount
            };
        }

        private static TestAddDiscountProductArguments GetCalculatorArgument(int amount, int discount = 0)
        {
            return new TestAddDiscountProductArguments()
            {
                ProductId = () => calculatorProductId,
                Price = () => calculatorProductPrice,
                Amount = () => amount,
                Discount = () => discount
            };
        }


        public static IEnumerable<TestCaseData> DataSuccessfulAddDiscount
        {

            get
            {


                // discount on specific product 

                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceProductDiscount(iphoneProductId, 10),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(2, 10)
                    }, 
                    "product discount"
                );

                // check prodcut that does not have the discount 
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceProductDiscount(iphoneProductId, 50),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetCalculatorArgument(2)
                    }, 
                    "product discount purchase other product"
                );

                // discount on all of the store 

                // one product 
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceStoreDiscount(10),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(3, 10)
                    }, 
                    "store discount one product"
                );

                // two products 
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceStoreDiscount(40),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1, 40), 
                        GetCalculatorArgument(2, 40)
                    }, 
                    "store discount two pdocuts"
                );

                // discount in date 

                // not the year
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceDateDiscount(40, DateTime.Now.Year - 3),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1)
                    }, 
                    "date discount not in year"
                );

                // this year
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceDateDiscount(40, DateTime.Now.Year),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1, 40)
                    }, 
                    "date discount in year"
                );

                // if then 

                // pred product amount 

                // amount enough (the needed amount) 
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new ServiceProductAmount(iphoneProductId, 2), 
                            new ServiceStoreDiscount(30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(2, 30)
                    }, 
                    "if amount, amount enough"
                );

                // amount not enough
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new ServiceProductAmount(iphoneProductAmount, 2),
                            new ServiceStoreDiscount(30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1)
                    }, 
                    "if amount, amount not enough"
                );

                // amount not enough, all products amount is enough 
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new ServiceProductAmount(iphoneProductAmount, 2),
                            new ServiceStoreDiscount(30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1), 
                        GetCalculatorArgument(1)
                    },
                    "if amount, amount not enough all products amount is enough"
                );

                // pred bag value

                // bag cost enough (the needed cost) 
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new ServiceBagValue(iphoneProductPrice + calculatorProductPrice),
                            new ServiceStoreDiscount(30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1, 30), 
                        GetCalculatorArgument(1, 30)
                    },
                    "if bag cost, cost enough"
                );

                // bag cost not enough
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new ServiceBagValue(iphoneProductPrice + 1),
                            new ServiceStoreDiscount(30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1)
                    },
                    "if bag cost, cost not enough"
                );

                // if then else 

                // bag amount enough (the needed amount)
                //  then storeDiscount
                //  else productDiscount
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceIf(
                            new ServiceProductAmount(iphoneProductId, 2),
                            new ServiceStoreDiscount(30), 
                            new ServiceProductDiscount(iphoneProductId, 30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(2),
                        GetCalculatorArgument(1)
                    },
                    "if then else, if is true"
                    // store discount 
                    , 30
                );

                // bag amount not enough
                //  then storeDiscount
                //  else productDiscount
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceIf(
                            new ServiceProductAmount(iphoneProductId, 2),
                            new ServiceStoreDiscount(30),
                            new ServiceProductDiscount(iphoneProductId, 30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1, 30),
                        GetCalculatorArgument(1)
                    },
                    "if then else, if is false"
                );

                // and

                // true and true
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO.ServiceAnd(
                                new ServiceProductAmount(iphoneProductId, 1),
                                new ServiceProductAmount(calculatorProductId, 1)
                            ), 
                            new ServiceProductDiscount(iphoneProductId, 30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1, 30),
                        GetCalculatorArgument(1)
                    },
                    "and, true and true"
                );

                // true and false
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO.ServiceAnd(
                                new ServiceProductAmount(iphoneProductId, 1),
                                new ServiceProductAmount(calculatorProductId, 2)
                            ),
                            new ServiceProductDiscount(iphoneProductId, 30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1),
                        GetCalculatorArgument(1)
                    },
                    "and, true and false"
                );

                // or

                // true and true
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO.ServiceOr(
                                new ServiceProductAmount(iphoneProductId, 1),
                                new ServiceProductAmount(calculatorProductId, 1)
                            ),
                            new ServiceProductDiscount(iphoneProductId, 30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1, 30),
                        GetCalculatorArgument(1)
                    },
                    "or, true and true"
                );

                // true and false
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO.ServiceOr(
                                new ServiceProductAmount(iphoneProductId, 1),
                                new ServiceProductAmount(calculatorProductId, 2)
                            ),
                            new ServiceProductDiscount(iphoneProductId, 30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1, 30),
                        GetCalculatorArgument(1)
                    },
                    "or, true and false"
                );

                // false and false
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO.ServiceOr(
                                new ServiceProductAmount(iphoneProductId, 2),
                                new ServiceProductAmount(calculatorProductId, 2)
                            ),
                            new ServiceProductDiscount(iphoneProductId, 30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1),
                        GetCalculatorArgument(1)
                    },
                    "or, false and false"
                );

                // xor

                // true and true
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new ServiceXor(
                                new ServiceProductAmount(iphoneProductId, 1),
                                new ServiceProductAmount(calculatorProductId, 1)
                            ),
                            new ServiceProductDiscount(iphoneProductId, 30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1),
                        GetCalculatorArgument(1)
                    },
                    "xor, true and true"
                );

                // true and false
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new ServiceXor(
                                new ServiceProductAmount(iphoneProductId, 1),
                                new ServiceProductAmount(calculatorProductId, 2)
                            ),
                            new ServiceProductDiscount(iphoneProductId, 30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1, 30),
                        GetCalculatorArgument(1)
                    },
                    "xor, true and false"
                );

                // false and false
                yield return AddDiscountTestCase(
                    // the discount
                    () => new ServiceConditionDiscount(
                            new ServiceXor(
                                new ServiceProductAmount(iphoneProductId, 2),
                                new ServiceProductAmount(calculatorProductId, 2)
                            ),
                            new ServiceProductDiscount(iphoneProductId, 30)
                        ),
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(1),
                        GetCalculatorArgument(1)
                    },
                    "xor, false and false"
                );

                // max over the discounts 

                // product1Discount, product2Discount
                yield return AddDiscountTestCase(
                    // the discount
                    () =>
                    {
                        ServiceMax discount = new ServiceMax();
                        discount.AddDiscount(new ServiceProductDiscount(iphoneProductId, 30));
                        discount.AddDiscount(new ServiceProductDiscount(calculatorProductId, 30));
                        return discount;
                    },
                    new List<TestAddDiscountProductArguments>()
                    {
                        // the shopping cart and expected discounts 
                        GetIphoneArgument(2, 30),
                        GetCalculatorArgument(2)
                    },
                    "max over the discounts, product1Discount, product2Discount"
                );

                // todo: nice to have, add tests with multiple discounts 

                // todo: nice to have, maybe add tests on the other arguments, such as manager requesting etc. 

            }
        }

        // todo: test remove discount policy 

        [Test]
        [TestCaseSource("DataSuccessfulAddDiscount")]
        public void SuccessfulAddDiscount(Func<IServiceExpression> discountExpression, string description, Func<int> storeId, Func<int> memberId, Func<StoreOwner_ManagerReqsTests, ServicePurchase> purchase, double expectedPrice)
        {
            Response<int> response = storeManagementFacade.AddDiscountPolicy(discountExpression(), description, storeId(), memberId());
            Assert.IsTrue(!response.ErrorOccured());
            int discountId = response.Value; 

            // checking that discount description was added 
            Response<IDictionary<int, string>> descriptionsResposne = buyerFacade.GetDiscountsDescriptions(storeId());
            Assert.IsTrue(!descriptionsResposne.ErrorOccured());
            Assert.IsTrue(descriptionsResposne.Value.Contains(new KeyValuePair<int, string>(discountId, description)));


            ServicePurchase resultPurchase = purchase(this);

            Assert.IsTrue(Math.Abs(expectedPrice - resultPurchase.purchasePrice) < 0.00001); 
        }

        // buying policies tests 

        public static IEnumerable<TestCaseData> DataFailedAddPurchasePolicy
        {
            get
            {
                string description = "amount should be ta least one";
                int amount = 1;
                // memberId is -1
                yield return new TestCaseData(
                    () => new ServiceAtlestAmount(iphoneProductId, amount),
                    description, () => storeId, () => -1);
                // guest id
                yield return new TestCaseData(
                    () => new ServiceAtlestAmount(iphoneProductId, amount),
                    description, () => storeId, () => guest1Id);
                // member id (does not have role in store) 
                yield return new TestCaseData(
                    () => new ServiceAtlestAmount(iphoneProductId, amount),
                    description, () => storeId, () => member1Id);
                // manager id no permission
                yield return new TestCaseData(
                    () => new ServiceAtlestAmount(iphoneProductId, amount),
                    description, () => storeId, () => member4Id); // not in defalut permissions 
                // coOwner in different store
                yield return new TestCaseData(
                    () => new ServiceAtlestAmount(iphoneProductId, amount),
                    description, () => store2Id, () => member5Id);

                // storeId is -1
                yield return new TestCaseData(
                    () => new ServiceAtlestAmount(iphoneProductId, amount),
                    description, () => -1, () => member3Id);

                // currently can add a discount to a product that is not currently in the store 
            }
        }

        [Test]
        [TestCaseSource("DataFailedAddPurchasePolicy")]
        public void FailedAddPurchasePolicy(Func<IServicePurchase> purchasePolicy, string description, Func<int> storeId, Func<int> memberId)
        {
            Response<int> response = storeManagementFacade.AddPurchasePolicy(purchasePolicy(), description, storeId(), memberId());
            Assert.IsTrue(response.ErrorOccured());

            // checking that policy description is not in purchases policies  

            Response<IDictionary<int, string>> descriptionsResposne = buyerFacade.GetPurchasePolicyDescriptions(storeId());
            if (!descriptionsResposne.ErrorOccured()) // the arguments can make an expected error here, for example storeId does not exists 
            {
                Assert.IsTrue(!descriptionsResposne.Value.Values.Contains(description));
            }

        }

        // helping classes for adding purchases policies tests 

        private class TestAddPurchasePolicyArguments
        {
            public Func<int> ProductId { get; set; }
            public Func<int> Amount { get; set; }
            public Func<bool> ShouldSucceedBuying { get; set; }
        }

        // helping functions for adding discount tests 
        // helping functions for adding discount tests 

        private static TestCaseData AddPurchasePolicyTestCase(Func<IServicePurchase> getPolicy, IList<AddProductToCartArguments> arguments, string description, bool shouldSucceedBuying)
        {
            return new TestCaseData(

                    // purchase policy
                    getPolicy, description,

                    // store and requesting (to add) member 
                    () => storeId, () => member3Id,
                    // adding to cart
                    GetPurchaseProcess(
                        arguments.Select(argumentObject =>
                            new AddProductToCartArguments()
                            {
                                ProductId = argumentObject.ProductId,
                                Amount = argumentObject.Amount
                            }).ToList(),

                        // expected result
                        shouldSucceedBuying
                    )
            );
        }


        public static IEnumerable<TestCaseData> DataSuccessfulAddPurchasePolicy
        {

            get
            {

                // restrictions tests 

                // after hour
                
                // is after hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceAfterHour(0),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId, 
                            Amount = () => 2
                        }
                    },
                    "is after hour",
                    // expected result of is the purchase successful 
                    false
                );

                // is not after hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceAfterHour(24),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "is not after hour",
                    // expected result of is the purchase successful 
                    true
                );

                // after hour specific product

                // product amount enough purchase after hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceAfterHourProduct(0, iphoneProductId, 2),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "product amount enough purchase after hour",
                    // expected result of is the purchase successful 
                    false
                );

                // product amount not enough purchase after hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceAfterHourProduct(0, iphoneProductId, 2),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 1
                        }
                    },
                    "product amount not enough purchase after hour",
                    // expected result of is the purchase successful 
                    true
                );

                // product amount enough from other products purchase after hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceAfterHourProduct(0, iphoneProductId, 2),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => calculatorProductId,
                            Amount = () => 2
                        }
                    },
                    "product amount enough from other products purchase after hour",
                    // expected result of is the purchase successful 
                    true
                );

                // product amount enough purchase after hour, not after hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceAfterHourProduct(24, iphoneProductId, 2),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "product amount enough purchase after hour, not after hour",
                    // expected result of is the purchase successful 
                    true
                );

                // before hour

                // is before hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceBeforeHour(23),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "is before hour",
                    // expected result of is the purchase successful 
                    false
                );

                // is not before hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceBeforeHour(-1),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "is not before hour",
                    // expected result of is the purchase successful 
                    true
                );

                // before hour specific product

                // product amount enough purchase before hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceBeforeHourProduct(23, iphoneProductId, 2),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "product amount enough purchase before hour",
                    // expected result of is the purchase successful 
                    false
                );

                // product amount not enough purchase before hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceBeforeHourProduct(23, iphoneProductId, 2),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 1
                        }
                    },
                    "product amount not enough purchase before hour",
                    // expected result of is the purchase successful 
                    true
                );

                // product amount enough from other products purchase before hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceBeforeHourProduct(23, iphoneProductId, 2),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => calculatorProductId,
                            Amount = () => 2
                        }
                    },
                    "product amount enough from other products purchase before hour",
                    // expected result of is the purchase successful 
                    true
                );

                // product amount enough purchase before hour, not before hour
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceBeforeHourProduct(-1, iphoneProductId, 2),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "product amount enough purchase before hour, not before hour",
                    // expected result of is the purchase successful 
                    true
                );

                // at least amount

                // product amount enough
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceAtlestAmount(iphoneProductId, 2),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "at least amount, product amount enough",
                    // expected result of is the purchase successful 
                    false
                );

                // product amount not enough
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceAtlestAmount(iphoneProductId, 2),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 1
                        }
                    },
                    "at least amount, product amount not enough",
                    // expected result of is the purchase successful 
                    true
                );

                // at most amount 

                // product amount at most as needed
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceAtMostAmount(iphoneProductId, 1),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 1
                        }
                    },
                    "product amount at most as needed",
                    // expected result of is the purchase successful 
                    false
                );

                // product amount more than what that is in at most
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceAtMostAmount(iphoneProductId, 1),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "// product amount more than what that is in at most",
                    // expected result of is the purchase successful 
                    true
                );

                // date (it means that can not buy on that date) 

                // in the date 
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceDateRestriction(DateTime.Now.Year, DateTime.Now.Month),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 1
                        }
                    },
                    "in the date",
                    // expected result of is the purchase successful 
                    false
                );

                // not in the date 
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceDateRestriction(DateTime.Now.Year - 1, DateTime.Now.Month),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "not in the date",
                    // expected result of is the purchase successful 
                    true
                );

                // implies (and predicates) 

                // amount is equal or more

                // false implies false 
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceImplies(
                        new ServiceCheckProductMore(iphoneProductId, 2),
                        new ServiceCheckProductMore(iphoneProductId, 2)
                        ), 
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 1
                        }
                    },
                    "amount is equal or more, false implies false",
                    // expected result of is the purchase successful 
                    true
                );

                // true implies true 
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceImplies(
                        new ServiceCheckProductMore(iphoneProductId, 2),
                        new ServiceCheckProductMore(iphoneProductId, 2)
                        ),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "amount is equal or more, true implies true",
                    // expected result of is the purchase successful 
                    true
                );

                // true does not imply false 
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceImplies(
                        new ServiceCheckProductMore(iphoneProductId, 2),
                        new ServiceCheckProductMore(iphoneProductId, 3)
                        ),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "amount is equal or more, true does not imply false",
                    // expected result of is the purchase successful 
                    false
                );

                // less than amount 

                // false implies false 
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceImplies(
                        new ServiceCheckProductLess(iphoneProductId, 3),
                        new ServiceCheckProductLess(iphoneProductId, 3)
                        ),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 3
                        }
                    },
                    "less than amount, false implies false",
                    // expected result of is the purchase successful 
                    true
                );

                // true implies true 
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceImplies(
                        new ServiceCheckProductLess(iphoneProductId, 2),
                        new ServiceCheckProductLess(iphoneProductId, 2)
                        ),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 1
                        }
                    },
                    "less than amount, true implies true",
                    // expected result of is the purchase successful 
                    true
                );

                // true does not imply false 
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new ServiceImplies(
                        new ServiceCheckProductLess(iphoneProductId, 2),
                        new ServiceCheckProductLess(iphoneProductId, 1)
                        ),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 1
                        }
                    },
                    "less than amount, true does not imply false",
                    // expected result of is the purchase successful 
                    false
                );

                // (notice in "and" and "or" these are between restictions) 

                // and 

                // true and true
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServiceAnd(
                        new ServiceAtlestAmount(iphoneProductId, 2),
                        new ServiceAtlestAmount(iphoneProductId, 2)
                    ), 
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 1
                        }
                    },
                    "true and true",
                    // expected result of is the purchase successful 
                    true
                );

                // true and false
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServiceAnd(
                        new ServiceAtlestAmount(iphoneProductId, 3),
                        new ServiceAtlestAmount(iphoneProductId, 2)
                    ),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "true and false",
                    // expected result of is the purchase successful 
                    false
                );

                // false and false
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServiceAnd(
                        new ServiceAtlestAmount(iphoneProductId, 2),
                        new ServiceAtlestAmount(iphoneProductId, 2)
                    ),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "false and false",
                    // expected result of is the purchase successful 
                    false
                );

                // or 

                // true or true
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServiceOr(
                        new ServiceAtlestAmount(iphoneProductId, 2),
                        new ServiceAtlestAmount(iphoneProductId, 2)
                    ),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 1
                        }
                    },
                    "true or true",
                    // expected result of is the purchase successful 
                    true
                );

                // true or false
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServiceOr(
                        new ServiceAtlestAmount(iphoneProductId, 3),
                        new ServiceAtlestAmount(iphoneProductId, 2)
                    ),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "true or false",
                    // expected result of is the purchase successful 
                    true
                );

                // false or false
                yield return AddPurchasePolicyTestCase(
                    // the purchase policy
                    () => new MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServiceOr(
                        new ServiceAtlestAmount(iphoneProductId, 2),
                        new ServiceAtlestAmount(iphoneProductId, 2)
                    ),
                    new List<AddProductToCartArguments>()
                    {
                        // the shopping cart 
                        new AddProductToCartArguments() {
                            ProductId = () => iphoneProductId,
                            Amount = () => 2
                        }
                    },
                    "false or false",
                    // expected result of is the purchase successful 
                    false
                );

                // todo: nice to have, add tests with multiple discounts 

                // todo: nice to have, maybe add tests on the other arguments, such as manager requesting etc. 

            }
        }

        // todo: test remove purchase policy 

        [Test]
        [TestCaseSource("DataSuccessfulAddPurchasePolicy")]
        public void SuccessfulAddPurchasePolicy(Func<IServicePurchase> purchasePolicy, string description, Func<int> storeId, Func<int> memberId, Func<StoreOwner_ManagerReqsTests, ServicePurchase> purchase)
        {
            Response<int> response = storeManagementFacade.AddPurchasePolicy(purchasePolicy(), description, storeId(), memberId());
            Assert.IsTrue(!response.ErrorOccured());
            int purchasePolicyId = response.Value;

            // checking that purchase policy description was added 
            Response<IDictionary<int, string>> descriptionsResposne = buyerFacade.GetPurchasePolicyDescriptions(storeId());
            Assert.IsTrue(!descriptionsResposne.ErrorOccured());
            Assert.IsTrue(descriptionsResposne.Value.Contains(new KeyValuePair<int, string>(purchasePolicyId, description)));


            ServicePurchase resultPurchase = purchase(this); // including checking if purchase succeeds 
        }



        // todo: maybe add tests to cc 6.1 and cc 6.2 

    }
}
