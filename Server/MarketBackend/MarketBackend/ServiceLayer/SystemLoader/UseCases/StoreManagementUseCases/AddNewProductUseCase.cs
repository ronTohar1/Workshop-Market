namespace MarketBackend.ServiceLayer
{
    internal class AddNewProductUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string StoreIdRef { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string ProductCategory { get; set; }
        public int Quantity { get; set; }

        public AddNewProductUseCase(string userIdRef, string storeIdRef, string productName, string productCategory, int price, int quantity, string ret = "_") : base("AddNewProduct", ret)
        {
            UserIdRef = userIdRef;
            StoreIdRef = storeIdRef;
            ProductName = productName;
            ProductCategory = productCategory;
            Price = price;
            Quantity = quantity;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            int storeId = (int)varsEnvironment[StoreIdRef];
            PrintUseCaseStarting();
            var res1 = systemOperator.GetStoreManagementFacade().Value.AddNewProduct(userId, storeId, ProductName, Price, ProductCategory);
            if (res1.IsErrorOccured())
            {
                Console.WriteLine("Unable to perform: " + this + "\nError message: " + res1.ErrorMessage);
                return false;
            }
            else
            {
                varsEnvironment[Ret] = res1.Value;
                Console.WriteLine($"Success: {Ret} = {varsEnvironment[Ret]}");
            }
            var res2 = systemOperator.GetStoreManagementFacade().Value.AddProductToInventory(userId, storeId, (int)varsEnvironment[Ret], Quantity);
            if (res2.IsErrorOccured())
            {
                Console.WriteLine("Unable to add quantity to inventory after adding new product." + "\nError message: " + res2.ErrorMessage);
                return false;
            }
            return true;
        }

        internal override string ArgsToString()
        {
            return string.Join(',', new object[] { UserIdRef, StoreIdRef, ProductName, ProductCategory, Price, Quantity });
        }
    }
}