namespace MarketBackend.ServiceLayer
{
    internal class AddBidUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string StoreIdRef { get; set; }
        public string ProductIdRef { get; set; }
        public int bidPrice { get; set; }

        public AddBidUseCase(string userIdRef, string storeIdRef, string productIdRef, int bidPrice, string ret = "_") : base("AddBid", ret)
        {
            UserIdRef = userIdRef;
            StoreIdRef = storeIdRef;
            ProductIdRef = productIdRef;
            this.bidPrice = bidPrice;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            int storeId = (int)varsEnvironment[StoreIdRef];
            int productId = (int)varsEnvironment[ProductIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetStoreManagementFacade().Value.AddBid(storeId, productId, userId, bidPrice);
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
            return string.Join(',', new object[] { UserIdRef, StoreIdRef, ProductIdRef, bidPrice });
        }
    }
}