namespace WebAPI.Requests
{
    public class AddNewProductToStoreRequest : StoreManagementRequest
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }

        public AddNewProductToStoreRequest(int userId, int storeId, string productName, double price, string category) : 
            base(userId, storeId)
        {
            ProductName = productName;
            Price = price;
            Category = category;
        }
    }
}
