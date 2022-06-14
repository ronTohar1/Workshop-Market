namespace WebAPI.Requests
{
    public class AddBidRequest :GeneralBidRequst
    {
        public double BidPrice { get; set; }
        public AddBidRequest(int storeId, int productId, int memberId, double bidPrice) : base(storeId, productId, memberId) {
            BidPrice=bidPrice;
        }
    }
}
