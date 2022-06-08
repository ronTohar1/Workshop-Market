namespace WebAPI.Requests
{
    public class PurchaseCartRequest : UserRequest
    {
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Holder { get; set; }
        public string Ccv { get; set; }
        public string Id { get; set; }
        public string ReceiverName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }

        public PurchaseCartRequest(
            int userId, 
            string cardNumber, 
            string month, 
            string year, 
            string holder,
            string ccv, 
            string id,
            string receiverName,
            string address,
            string city,
            string country,
            string zip) : base(userId)
        {
            CardNumber = cardNumber;
            Month = month;
            Year = year;
            Holder = holder;
            Ccv = ccv;
            Id = id;
            ReceiverName = receiverName;
            Address = address;
            City = city;
            Country = country;
            Zip = zip;
        }
    }
}
