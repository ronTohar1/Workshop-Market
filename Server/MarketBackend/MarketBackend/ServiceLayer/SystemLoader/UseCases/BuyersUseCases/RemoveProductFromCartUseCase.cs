namespace MarketBackend.ServiceLayer
{
    internal class RemoveProductFromCartUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string StoreIdRef { get; set; }
        public string ProductIdRef { get; set; }

        public RemoveProductFromCartUseCase(string userIdRef, string storeIdRef, string productIdRef, string ret = "_") : base("RemoveProductFromCart", ret)
        {
            UserIdRef = userIdRef;
            StoreIdRef = storeIdRef;
            ProductIdRef = productIdRef;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            int storeId = (int)varsEnvironment[StoreIdRef];
            int productId = (int)varsEnvironment[ProductIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetBuyerFacade().Value.RemoveProductFromCart(userId, storeId, productId);
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
            return string.Join(',', new object[] { UserIdRef, StoreIdRef, ProductIdRef });
        }
    }
}