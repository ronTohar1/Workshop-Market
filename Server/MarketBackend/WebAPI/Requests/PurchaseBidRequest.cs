namespace WebAPI.Requests
{
    public class PurchaseBidRequest : PurchaseRequest
    {

        public int StoreId { get; set; }
        public int BidId { get; set; }

        public PurchaseBidRequest(int storeId, int bidId, int userId, string cardNumber, string month, string year, string holder, string ccv, string id, string receiverName, string address, string city, string country, string zip) : base(userId, cardNumber, month, year, holder, ccv, id, receiverName, address, city, country, zip)
        {
            StoreId = storeId;
            BidId = bidId;
        }

    }
}
