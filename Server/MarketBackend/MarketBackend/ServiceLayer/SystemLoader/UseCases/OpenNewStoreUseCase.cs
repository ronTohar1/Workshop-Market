namespace MarketBackend.ServiceLayer
{
    internal class OpenNewStoreUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string StoreName { get; set; }

        public OpenNewStoreUseCase(string userIdRef, string storeName, string ret = "_") : base("OpenNewStore", ret)
        {
            UserIdRef = userIdRef;
            StoreName = storeName;
        }

        internal override void Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetStoreManagementFacade().Value.OpenNewStore(userId, StoreName);
            if (res.IsErrorOccured())
                Console.WriteLine("Unable to perform: " + this + "\nError message: " + res.ErrorMessage);
            else
            {
                varsEnvironment[Ret] = res.Value;
                Console.WriteLine($"Success: {Ret} = {varsEnvironment[Ret]}");
            }
        }

        internal override string ArgsToString()
        {
            return string.Join(',', new object[] { UserIdRef, StoreName });
        }
    }
}