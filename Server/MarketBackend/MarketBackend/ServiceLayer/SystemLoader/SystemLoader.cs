using MarketBackend.BusinessLayer.Market.StoreManagment;

namespace MarketBackend.ServiceLayer
{
    internal class SystemLoader
    {
        private ScenarioDTO Scenario;
        private SystemOperator SystemOperator;
        private IDictionary<string, object> VarsEnvironment;
        public string AdminUsername { get { return Scenario.AdminUsername; } }
        public string AdminPassword { get { return Scenario.AdminPassword; } }

        public SystemLoader(string initFilePath, SystemOperator systemOperator)
        {
            MakePresets();
            Scenario = ScenarioParser.Parse(initFilePath);
            VarsEnvironment = new Dictionary<string, object>();
            SystemOperator = systemOperator;
        }

        internal void LoadSystem()
        {
            foreach (var usecase in Scenario.UseCases)
                usecase.Apply(SystemOperator, VarsEnvironment);
        }


        #region MakePresets
        private ScenarioDTO InitScenario() // from email
        {
            var actions = new UseCase[]
            {
                new RegisterUseCase("u2", "password2", ret: "id2"),
                new RegisterUseCase("u3", "password3", ret: "id3"),
                new RegisterUseCase("u4", "password4", ret: "id4"),
                new RegisterUseCase("u5", "password5", ret: "id5"),
                new RegisterUseCase("u6", "password6", ret: "id6"),
                new LoginUseCase("u2", "password2"),
                new LoginUseCase("u3", "password3"),
                new LoginUseCase("u4", "password4"),
                new LoginUseCase("u5", "password5"),
                new LoginUseCase("u6", "password6"),
                new OpenNewStoreUseCase("id2", "store1", ret: "s1"),
                new AddNewProductUseCase("id2", "s1", "bamba", "snacks", 30, 20, ret: "p1"),
                new MakeCoManagerUseCase("id2", "id3", "s1"),
                new ChangeManagerPermissionUseCase("id2", "id3", "s1", new List<Permission>(new Permission[] {Permission.ManageInventory})),
                new MakeCoOwnerUseCase("id2", "id4", "s1"),
                new MakeCoOwnerUseCase("id2", "id5", "s1"),
                new LogoutUseCase("id2")
            };

            return new ScenarioDTO("u1", "password1", actions);
        }

        private ScenarioDTO Scenario1()
        {
            var actions = new UseCase[]
            {
                new RegisterUseCase("u1", "password1", ret: "id1"),
                new RegisterUseCase("u2", "password2", ret: "id2"),
                new RegisterUseCase("u3", "password3", ret: "id3"),
                new LoginUseCase("u1", "password1"),
                new LoginUseCase("u2", "password2"),
                new LoginUseCase("u3", "password3"),
                new OpenNewStoreUseCase("id1", "store1", ret: "s1"),
                new MakeCoOwnerUseCase("id1", "id2", "s1"),
                new MakeCoOwnerUseCase("id2", "id3", "s1"),
                new RemoveCoOwnerUseCase("id3", "id2", "s1"),
                new RemoveCoOwnerUseCase("id1", "id2", "s1"),
            };

            return new ScenarioDTO("u0", "password0", actions);
        }

        private ScenarioDTO Scenario2()
        {
            var actions = new UseCase[]
            {
                new RegisterUseCase("u1", "password1", ret: "id1"),
                new RegisterUseCase("u2", "password2", ret: "id2"),
                new RegisterUseCase("u3", "password3", ret: "id3"),
                new LoginUseCase("u1", "password1"),
                new LoginUseCase("u2", "password2"),
                new LoginUseCase("u3", "password3"),
                new OpenNewStoreUseCase("id1", "store1", ret: "s1"),
                new MakeCoOwnerUseCase("id1", "id2", "s1"),
                new OpenNewStoreUseCase("id3", "store2", ret: "s2"),
                new MakeCoOwnerUseCase("id3", "id1", "s2"),
                new MakeCoOwnerUseCase("id3", "id2", "s2"),
                new RemoveCoOwnerUseCase("id1", "id2", "s2"),
            };

            return new ScenarioDTO("u0", "password0", actions);
        }

        private void MakePresets()
        {
            var scenarios = new KeyValuePair<string, ScenarioDTO>[] 
            { 
                new KeyValuePair<string, ScenarioDTO>("init", InitScenario()), 
                new KeyValuePair<string, ScenarioDTO>("scenario1", Scenario1()), 
                new KeyValuePair<string, ScenarioDTO>("scenario2", Scenario2()), 
            };
            foreach (var scenario in scenarios)
            {
                ScenarioParser.SaveAsFile(scenario.Key + ".json", scenario.Value);
            }
        }

        #endregion
    }
}