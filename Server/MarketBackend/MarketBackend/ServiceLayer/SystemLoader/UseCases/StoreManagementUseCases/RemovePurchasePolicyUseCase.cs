namespace MarketBackend.ServiceLayer
{
    internal class RemovePurchasePolicyUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string StoreIdRef { get; set; }
        public string PolicyId { get; set; }

        public RemovePurchasePolicyUseCase(string userIdRef, string storeIdRef, string policyId, string ret = "_") : base("RemovePurchasePolicy", ret)
        {
            UserIdRef = userIdRef;
            StoreIdRef = storeIdRef;
            PolicyId = policyId;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            int storeId = (int)varsEnvironment[StoreIdRef];
            int policyId = (int)varsEnvironment[PolicyId];
            PrintUseCaseStarting();
            var res = systemOperator.GetStoreManagementFacade().Value.RemovePurchasePolicy(policyId, storeId, userId);
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
            return string.Join(',', new object[] { UserIdRef, StoreIdRef, PolicyId });
        }
    }
}