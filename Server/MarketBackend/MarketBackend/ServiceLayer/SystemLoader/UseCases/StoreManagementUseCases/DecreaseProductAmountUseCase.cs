namespace MarketBackend.ServiceLayer
{
    internal class DecreaseProductAmountUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string StoreIdRef { get; set; }
        public string ProductIdRef { get; set; }
        public int Amount { get; set; }

        public DecreaseProductAmountUseCase(string userIdRef, string storeIdRef, string productIdRef, int amount, string ret = "_") : base("DecreaseProductAmount", ret)
        {
            UserIdRef = userIdRef;
            StoreIdRef = storeIdRef;
            ProductIdRef = productIdRef;
            Amount = amount;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            int storeId = (int)varsEnvironment[StoreIdRef];
            int productId = (int)varsEnvironment[ProductIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetStoreManagementFacade().Value.DecreaseProduct(userId, storeId, productId, Amount);
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
            return string.Join(',', new object[] { UserIdRef, StoreIdRef, ProductIdRef, Amount });
        }
    }
}