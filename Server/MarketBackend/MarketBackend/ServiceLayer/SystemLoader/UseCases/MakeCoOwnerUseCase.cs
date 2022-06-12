namespace MarketBackend.ServiceLayer
{
    internal class MakeCoOwnerUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string TargetUserIdRef { get; set; }
        public string StoreIdRef { get; set; }

        public MakeCoOwnerUseCase(string userIdRef, string targerUserIdRef, string storeIdRef, string ret = "_") : base("MakeCoOwner", ret)
        {
            UserIdRef = userIdRef;
            TargetUserIdRef = targerUserIdRef;
            StoreIdRef = storeIdRef;
        }

        internal override void Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            int targetUserId = (int)varsEnvironment[TargetUserIdRef];
            int storeId = (int)varsEnvironment[StoreIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetStoreManagementFacade().Value.MakeCoOwner(userId, targetUserId, storeId);
            if (res.IsErrorOccured())
                Console.WriteLine("Unable to perform: " + this + "\nError message: " + res.ErrorMessage);
            else
            {
                varsEnvironment[Ret] = res.Value;
                Console.WriteLine("Success");
            }
        }

        internal override string ArgsToString()
        {
            return string.Join(',', new object[] { UserIdRef, TargetUserIdRef, StoreIdRef });
        }
    }
}