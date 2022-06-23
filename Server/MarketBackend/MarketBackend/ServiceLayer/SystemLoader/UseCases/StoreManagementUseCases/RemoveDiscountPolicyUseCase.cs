namespace MarketBackend.ServiceLayer
{
    internal class RemoveDiscountPolicyUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string StoreIdRef { get; set; }
        public string DiscountIdRef { get; set; }

        public RemoveDiscountPolicyUseCase(string userIdRef, string storeIdRef, string discountIdRef, string ret = "_") : base("RemoveDiscountPolicy", ret)
        {
            UserIdRef = userIdRef;
            StoreIdRef = storeIdRef;
            DiscountIdRef = discountIdRef;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            int storeId = (int)varsEnvironment[StoreIdRef];
            int discountId = (int)varsEnvironment[DiscountIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetStoreManagementFacade().Value.RemoveDiscountPolicy(discountId, storeId, userId);
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
            return string.Join(',', new object[] { UserIdRef, StoreIdRef, DiscountIdRef });
        }
    }
}