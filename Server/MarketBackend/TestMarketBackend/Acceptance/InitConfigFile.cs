using MarketBackend.ServiceLayer.ServiceDTO;
using NUnit.Framework;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.ServiceLayer;
using MarketBackend.BusinessLayer.Market.StoreManagment;

namespace TestMarketBackend.Acceptance
{
    internal class InitConfigFile
    {
        [Test]
        public void InitConfigFileTest_Pass()
        {
            ScenarioDTO scenario = new ScenarioDTO("u1", "p1",
                new UseCase[] {
                    new RegisterUseCase("u2", "password2", ret: "id2"),
                    new RegisterUseCase("u3", "password3", ret: "id3"),
                    new LoginUseCase("u2", "password2"),
                    new LoginUseCase("u3", "password3"),
                    new OpenNewStoreUseCase("id2", "store1", ret: "s1"),
                    new AddNewProductUseCase("id2", "s1", "bamba", "snacks", 30, 20, ret: "p1"),
                    new MakeCoManagerUseCase("id2", "id3", "s1"),
                    new ChangeManagerPermissionUseCase("id2", "id3", "s1", new List<Permission>(new Permission[] {Permission.ManageInventory})),
                    new IncreaseProductAmountUseCase("id3", "s1", "p1", 50),
                    new LogoutUseCase("id2"),
                    new LogoutUseCase("id3"),
                    new MakeRefUseCase<int>(0, "idAdmin"),
                    new LoginUseCase("u2", "password2"),
                    new LoginUseCase("u3", "password3"),
                    new EnterUseCase(ret: "guestId"),
                    new AddProductToCartUseCase("guestId", "s1", "p1", 5),
                    new ChangeProductAmountUseCase("guestId", "s1", "p1", 10),
                    new PurchaseCartUseCase("guestId", "card", "08", "2023", "111", "222", "aaa", "aaaa", "aaaa", "acca", "xaxax", "aaa"),
                    new LeaveUseCase("guestId"),
                });
            SystemOperator systemOperator = new SystemOperator(scenario.AdminUsername, scenario.AdminPassword, false);
            SystemLoader systemLoader = new SystemLoader(scenario, systemOperator);
            systemLoader.LoadSystem(assertSuccess: true);
        }

        [Test]
        public void InitConfigFileTest_Fail_RemoveMember()
        {
            ScenarioDTO scenario = new ScenarioDTO("u1", "p1",
                new UseCase[] {
                    new RegisterUseCase("u2", "password2", ret: "id2"),
                    new RegisterUseCase("u3", "password3", ret: "id3"),
                    new LoginUseCase("u2", "password2"),
                    new LoginUseCase("u3", "password3"),
                    new OpenNewStoreUseCase("id2", "store1", ret: "s1"),
                    new AddNewProductUseCase("id2", "s1", "bamba", "snacks", 30, 20, ret: "p1"),
                    new MakeCoManagerUseCase("id2", "id3", "s1"),
                    new IncreaseProductAmountUseCase("id3", "s1", "p1", 50),        // no permissions
                });
            SystemOperator systemOperator = new SystemOperator(scenario.AdminUsername, scenario.AdminPassword, false);
            SystemLoader systemLoader = new SystemLoader(scenario, systemOperator);
            Assert.Throws<Exception>(() => systemLoader.LoadSystem(assertSuccess: true));
        }
    }
}
