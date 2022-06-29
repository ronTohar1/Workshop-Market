using MarketBackend.BusinessLayer.Market.StoreManagment;

namespace MarketBackend.ServiceLayer
{
    internal class ChangeManagerPermissionUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string TargetUserIdRef { get; set; }
        public string StoreIdRef { get; set; }
        public IList<Permission> Permissions { get; set; }

        public ChangeManagerPermissionUseCase(string userIdRef, string targerUserIdRef, string storeIdRef, IList<Permission> permissions, string ret = "_") : base("ChangeManagerPermission", ret)
        {
            UserIdRef = userIdRef;
            TargetUserIdRef = targerUserIdRef;
            StoreIdRef = storeIdRef;
            Permissions = permissions;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            int targetUserId = (int)varsEnvironment[TargetUserIdRef];
            int storeId = (int)varsEnvironment[StoreIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetStoreManagementFacade().Value.ChangeManagerPermission(userId, targetUserId, storeId, Permissions);
            if (res.IsErrorOccured())
                Console.WriteLine("Unable to perform: " + this + "\nError message: " + res.ErrorMessage);
            else
            {
                varsEnvironment[Ret] = res.Value;
                Console.WriteLine("Success");
                return true;
            }
            return false;
        }

        internal override string ArgsToString()
        {
            return string.Join(',', new object[] { UserIdRef, TargetUserIdRef, StoreIdRef, $"[{string.Join(',', Permissions)}]" });
        }
    }
}

