namespace MarketBackend.ServiceLayer
{
    internal class ChangeProductAmountUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string StoreIdRef { get; set; }
        public string ProductIdRef { get; set; }
        public int NewAmount { get; set; }

        public ChangeProductAmountUseCase(string userIdRef, string storeIdRef, string productIdRef, int newAmount, string ret = "_") : base("ChangeProductAmount", ret)
        {
            UserIdRef = userIdRef;
            StoreIdRef = storeIdRef;
            ProductIdRef = productIdRef;
            NewAmount = newAmount;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            int storeId = (int)varsEnvironment[StoreIdRef];
            int productId = (int)varsEnvironment[ProductIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetBuyerFacade().Value.changeProductAmountInCart(userId, storeId, productId, NewAmount);
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
            return string.Join(',', new object[] { UserIdRef, StoreIdRef, ProductIdRef, NewAmount });
        }
    }
}