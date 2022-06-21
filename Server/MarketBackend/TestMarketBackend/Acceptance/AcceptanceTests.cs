using MarketBackend.ServiceLayer;
using MarketBackend.ServiceLayer.ServiceDTO;
using NUnit.Framework;
using NLog;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Net.Http;

namespace TestMarketBackend.Acceptance
{
    internal class AcceptanceTests
    {
        // system operator
        private SystemOperator systemOperator;

        // facades
        protected BuyerFacade buyerFacade;
        protected StoreManagementFacade storeManagementFacade;
        protected AdminFacade adminFacade;
        protected ExternalSystemFacade externalSystemFacade;

        // users
        protected static int guest1Id;
        protected static int guest2Id;
        protected static int guest3Id;
        protected static int guest4Id;
        protected static int guest5Id;
        protected static int guest6Id;
        protected static int guest7Id;
        protected static int member1Id;
        protected static int member2Id;
        protected static int member3Id;
        protected static int member4Id;
        protected static int member5Id;
        protected static int member6Id;
        protected static int member7Id;
        protected const string userName1 = "userName1";
        protected const string password1 = "password";
        protected const string userName2 = "userName2";
        protected const string password2 = "password2";
        protected const string userName3 = "userName3";
        protected const string password3 = "password3";
        protected const string userName4 = "userName4";
        protected const string password4 = "password4";
        protected const string userName5 = "userName5";
        protected const string password5 = "password5";
        protected const string userName6 = "userName6";
        protected const string password6 = "password6";
        protected const string userName7 = "userName7";
        protected const string password7 = "password7";

        protected static ConcurrentQueue<string> member1Notifications = new ConcurrentQueue<string>();
        protected static ConcurrentQueue<string> member2Notifications = new ConcurrentQueue<string>();
        protected static ConcurrentQueue<string> member3Notifications = new ConcurrentQueue<string>();
        protected static ConcurrentQueue<string> member4Notifications = new ConcurrentQueue<string>();
        protected static ConcurrentQueue<string> member5Notifications = new ConcurrentQueue<string>();
        protected static ConcurrentQueue<string> member6Notifications = new ConcurrentQueue<string>();
        protected static ConcurrentQueue<string> member7Notifications = new ConcurrentQueue<string>();

        protected static ConcurrentQueue<string>[] membersNotifications = new ConcurrentQueue<string>[]
        {
            member1Notifications, member2Notifications, member3Notifications, member4Notifications, member5Notifications, member6Notifications, member7Notifications
        };

        //Admin
        protected const string adminUsername = "admin";
        protected const string adminPassword = "admin";
        protected static int adminId;

        // stores
        protected static int storeOwnerId;
        protected static int storeId;
        protected const string storeName = "TheStore";
        protected static int store2OwnerId;
        protected static int store2Id;
        protected const string store2Name = "TheSecondStore";

        protected static int[] storesIds;

        // products
        // categories
        protected const string mobileCategory = "Mobile";
        protected const string officeCategory = "Office";
        protected const string foodCategory = "Food";
            
        // first store products
        protected const string iphoneProductName = "Iphone13";
        protected const int iphoneProductPrice = 4000;
        protected const int iphoneProductAmount = 50;
        protected static int iphoneProductId;

        protected const string calculatorProductName = "991ES Calculator";
        protected const int calculatorProductPrice = 100;
        protected const int calculatorProductAmount = 200;
        protected static int calculatorProductId;

        protected const string bambaProductName = "Bamba";
        protected const int bambaProductPrice = 30;
        protected const int bambaProductAmount = 20;
        protected static int bambaProductId;

        // second store products
        protected const string galaxyProductName = "Galaxy S22";
        protected const int galaxyProductPrice = 4000;
        protected const int galaxyProductAmount = 60;
        protected static int galaxyProductId;

        protected const string usbChargerProductName = "USB Charger";
        protected const int usbChargerProductPrice = 50;
        protected const int usbChargerProductAmount = 250;
        protected static int usbChargerProductId;

        protected const string portableChargerProductName = "Portable Charger";
        protected const int portableChargerProductPrice = 75;
        protected const int portableChargerProductAmount = 150;
        protected static int portableChargerProductId;

        protected const string phoneCaseProductName = "Phone Case";
        protected const int phoneCaseProductPrice = 80;
        protected const int phoneCaseProductAmount = 120;
        protected static int phoneCaseProductId;

        // payment details
        protected static ServicePaymentDetails paymentDetails =
            new ServicePaymentDetails("2222333344445555", "12", "2025", "Yossi Cohen", "262", "20444444");

        protected static ServiceSupplyDetails supplyDetails =
            new ServiceSupplyDetails("Yossi Cohen", "Rager 100", "Beer Sheva", "Israel", "8458527");

        private void SetUpUsers()
        {
            // Both guests register as members
            // guest1 stays in the market as a guest
            // member1 logs in
            Response<int> guest1IdResponse = buyerFacade.Enter();
            guest1Id = guest1IdResponse.Value;
            Response<int> member1IdResponse = buyerFacade.Register(userName1, password1);
            member1Id = member1IdResponse.Value;

            Response<int> guest2IdResponse = buyerFacade.Enter();
            guest2Id = guest2IdResponse.Value;
            Response<int> member2IdResponse = buyerFacade.Register(userName2, password2);
            member2Id = member2IdResponse.Value;

            Response<int> guest3IdResponse = buyerFacade.Enter();
            guest3Id = guest3IdResponse.Value;
            Response<int> member3IdResponse = buyerFacade.Register(userName3, password3);
            member3Id = member3IdResponse.Value;

            Response<int> guest4IdResponse = buyerFacade.Enter();
            guest4Id = guest4IdResponse.Value;
            Response<int> member4IdResponse = buyerFacade.Register(userName4, password4);
            member4Id = member4IdResponse.Value;

            Response<int> guest5IdResponse = buyerFacade.Enter();
            guest5Id = guest5IdResponse.Value;
            Response<int> member5IdResponse = buyerFacade.Register(userName5, password5);
            member5Id = member5IdResponse.Value;

            Response<int> guest6IdResponse = buyerFacade.Enter();
            guest6Id = guest6IdResponse.Value;
            Response<int> member6IdResponse = buyerFacade.Register(userName6, password6);
            member6Id = member6IdResponse.Value;

            Response<int> guest7IdResponse = buyerFacade.Enter();
            guest7Id = guest7IdResponse.Value;
            Response<int> member7IdResponse = buyerFacade.Register(userName7, password7);
            member7Id = member7IdResponse.Value;

            buyerFacade.Login(userName2, password2, GetNotificationsFunction(member2Notifications));
            buyerFacade.Login(userName3, password3, GetNotificationsFunction(member3Notifications));
            buyerFacade.Login(userName5, password5, GetNotificationsFunction(member5Notifications));
        }

        protected Func<string[], bool> GetNotificationsFunction(ConcurrentQueue<string> currentNotifications)
        {
            return newNotifications =>
            {
                foreach (string notification in newNotifications)
                {
                    currentNotifications.Enqueue(notification);
                }
                return true;
            };
        }

        public void SetUpStores()
        {
            // Opening a store whose owner is member2
            storeOwnerId = member2Id;
            Response<int> serviceStoreIdResponse = storeManagementFacade.OpenNewStore(storeOwnerId, storeName);
            storeId = serviceStoreIdResponse.Value;

            // the owner(member2) appoints member3 as a store owner
            Response<bool> response = storeManagementFacade.MakeCoOwner(member2Id, member3Id, storeId);

            // the owner(member2) appoints member4 as a store manager
            response = storeManagementFacade.MakeCoManager(member2Id, member4Id, storeId);

            // the owner(member2) appoints member5 as a store owner 
            response = storeManagementFacade.MakeCoOwner(member2Id, member5Id, storeId);

            // a coOwner(member5) appoints member6 as a store owner 
            response = storeManagementFacade.MakeCoOwner(member5Id, member6Id, storeId);

            // a coOwner(member5) appoints member7 as a manager 
            response = storeManagementFacade.MakeCoManager(member5Id, member7Id, storeId);

            // Opening a store whose owner is member2
            store2OwnerId = member2Id; 
            serviceStoreIdResponse = storeManagementFacade.OpenNewStore(store2OwnerId, store2Name);
            store2Id = serviceStoreIdResponse.Value;

            // notice that member2 is a store owner in all stores
            storesIds = new int[] { storeId, store2Id }; 
        }

        private void SetUpStore1Inventory()
        {
            // Adding Iphones to the store
            Response<int> iphoneProductIdResponse = storeManagementFacade.AddNewProduct(storeOwnerId, storeId, iphoneProductName, iphoneProductPrice, mobileCategory);
            iphoneProductId = iphoneProductIdResponse.Value;
            storeManagementFacade.AddProductToInventory(storeOwnerId, storeId, iphoneProductId, iphoneProductAmount);

            // Adding calculators to the store but there arent any in the stock
            Response<int> calculatorProductIdResponse = storeManagementFacade.AddNewProduct(storeOwnerId, storeId, calculatorProductName, calculatorProductPrice, officeCategory);
            calculatorProductId = calculatorProductIdResponse.Value;
            storeManagementFacade.AddProductToInventory(storeOwnerId, storeId, calculatorProductId, calculatorProductAmount);

            // Adding Bambas to the store
            Response<int> bambaProductIdResponse = storeManagementFacade.AddNewProduct(storeOwnerId, storeId, bambaProductName, bambaProductPrice, foodCategory);
            bambaProductId = bambaProductIdResponse.Value;
            storeManagementFacade.AddProductToInventory(storeOwnerId, storeId, bambaProductId, bambaProductAmount);
        }

        public void SetUpStore2Inventory()
        {
            // Adding Galaxies to the store
            Response<int> galaxyProductIdResponse = storeManagementFacade.AddNewProduct(store2OwnerId, store2Id, galaxyProductName, galaxyProductPrice, mobileCategory);
            galaxyProductId = galaxyProductIdResponse.Value;
            storeManagementFacade.AddProductToInventory(store2OwnerId, store2Id, galaxyProductId, galaxyProductAmount);

            // Adding USB chargers to the store
            Response<int> usbChargerProductIdResponse = storeManagementFacade.AddNewProduct(store2OwnerId, store2Id, usbChargerProductName, usbChargerProductPrice, mobileCategory);
            usbChargerProductId = usbChargerProductIdResponse.Value;
            storeManagementFacade.AddProductToInventory(store2OwnerId, store2Id, usbChargerProductId, usbChargerProductAmount);

            // Adding USB chargers to the store
            Response<int> portableChargerProductIdResponse = storeManagementFacade.AddNewProduct(store2OwnerId, store2Id, portableChargerProductName, portableChargerProductPrice, mobileCategory);
            portableChargerProductId = portableChargerProductIdResponse.Value;
            storeManagementFacade.AddProductToInventory(store2OwnerId, store2Id, portableChargerProductId, portableChargerProductAmount);

            // Adding USB chargers to the store
            Response<int> phoneCaseProductIdResponse = storeManagementFacade.AddNewProduct(store2OwnerId, store2Id, phoneCaseProductName, phoneCaseProductPrice, mobileCategory);
            phoneCaseProductId = phoneCaseProductIdResponse.Value;
            storeManagementFacade.AddProductToInventory(store2OwnerId, store2Id, phoneCaseProductId, phoneCaseProductAmount);
        }

        public void SetUpStoresInventories()
        {
            SetUpStore1Inventory();

            SetUpStore2Inventory();
            
        }

        protected void SetUpShoppingCarts()
        {
            buyerFacade.AddProdcutToCart(member3Id, storeId, iphoneProductId, 2);
            buyerFacade.AddProdcutToCart(member3Id, storeId, calculatorProductId, 5);
            buyerFacade.AddProdcutToCart(member3Id, store2Id, galaxyProductId, 1);
            buyerFacade.AddProdcutToCart(member3Id, store2Id, usbChargerProductId, 1);
            buyerFacade.AddProdcutToCart(member3Id, store2Id, portableChargerProductId, 2);
        }

        // removing the current notifications that could have occured becuase of the setup or in other tests
        public void SetUpNotificationQueues()
        {
            member1Notifications.Clear();
            member2Notifications.Clear();
            member3Notifications.Clear();
            member4Notifications.Clear();
            member5Notifications.Clear();
            member6Notifications.Clear();
            member7Notifications.Clear();
        }

        [SetUp]
        public void SetUp()
        {
            SystemOperator.RemoveAllDatabaseContent();

            SetUpFacaedes(false); 

            SetUpUsers();

            SetUpStores();

            SetUpStoresInventories();

            SetUpNotificationQueues(); 

        }

        private void SetUpFacaedes(bool loadFromDatabase)
        {
            systemOperator = new SystemOperator(adminUsername, adminPassword, loadFromDatabase);
            adminId = systemOperator.MarketOpenerAdminId;
            if (adminId < 0)
                throw new Exception("Unable to open market successfully");

            buyerFacade = systemOperator.GetBuyerFacade().Value;
            storeManagementFacade = systemOperator.GetStoreManagementFacade().Value;
            adminFacade = systemOperator.GetAdminFacade().Value;
            externalSystemFacade = systemOperator.GetExternalSystemFacade().Value;
        }

        [TearDown]
        public void TearDown()
        {
            systemOperator.CloseMarket();
        }


        protected void ReopenMarket()
        {
            // todo: do this only if using the database (according to the configuration) 
            systemOperator.CloseMarket();

            SetUpFacaedes(true); // loading from database 
        }


        protected Response<T>[] GetResponsesFromThreads<T>(Func<Response<T>>[] jobs)
        {
            Response<T>[] responses = new Response<T>[jobs.Length];
            Thread[] threads = new Thread[jobs.Length];

            for(int i = 0; i < jobs.Length; i++)
            {
                int temp = i;
                threads[i] = new Thread(() => { responses[temp] = jobs[temp](); });
                threads[i].Start();
            }

            foreach(Thread thread in threads)
                thread.Join();

            return responses;
        }

        protected int[] GetFreshMembersIds(int usersNumber)
        {
            int[] ids = new int[usersNumber];

            for (int i = 0; i < usersNumber; i++)
            {
                Response<int> enterResponse = buyerFacade.Enter();
                Response<int> registerResponse = buyerFacade.Register($"Name_{i}", $"Pass_{i}");
                Response<int> loginResponse = buyerFacade.Login($"Name_{i}", $"Pass_{i}", newNotifications => true);

                if (enterResponse.IsErrorOccured() || registerResponse.IsErrorOccured() || loginResponse.IsErrorOccured())
                    throw new Exception("Unexpected erorr occured: 'GetFreshMembersIds'");
                ids[i] = registerResponse.Value;
            }

            return ids;
        }

        protected bool Exactly1ResponseIsSuccessful<T>(Response<T>[] responses)
        {
            return responses.Where(r => !r.IsErrorOccured()).Count() == 1;
        }

        protected bool SameElements<T>(IList<T> list1, IList<T> list2)
        {
            if (list1.Count != list2.Count)
                return false;
            return list1.All(element => list2.Contains(element));

        }

        protected bool AreEqualMaybeNull<T>(T elemnet1, T element2)
        {
            if (elemnet1 == null)
                return element2 == null; 
            return elemnet1.Equals(element2);
        }

        protected bool SameElementsMaybeNull<T>(IList<T> list1, IList<T> list2)
        {
            if (list1 == null)
                return list2 == null;
            return SameElements(list1, list2);
        }

        protected bool SameDictionaries<T, E>(IDictionary<T, E> dictionary1, IDictionary<T, E> dictionary2)
        {
            if (!SameElements(dictionary1.Keys.ToList(), dictionary2.Keys.ToList()))
            {
                return false; 
            }
            foreach (T key in dictionary1.Keys)
            {
                if (!AreEqualMaybeNull(dictionary1[key], dictionary2[key]))
                {
                    return false;
                }
            }
            return true; 
        }

        protected bool SameDictionariesWithLists<T, E>(IDictionary<T, IList<E>> dictionary1, IDictionary<T, IList<E>> dictionary2)
        {
            if (!SameElements(dictionary1.Keys.ToList(), dictionary2.Keys.ToList()))
            {
                return false;
            }
            foreach (T key in dictionary1.Keys)
            {
                if (!SameElementsMaybeNull(dictionary1[key], dictionary2[key]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
