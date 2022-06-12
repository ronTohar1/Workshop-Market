namespace WebAPI.Requests
{
    public class MakeCounterOfferRequest
    {
        public int StoreId { get; set; }
        public int MemberId { get; set; }
        public int BidId { get; set; }
        public double Offer { get; set; }
        public MakeCounterOfferRequest(int storeId, int memberId, int bidId, double offer)
        {
            StoreId = storeId;
            MemberId = memberId;
            BidId = bidId;
            Offer = offer;
        }
    }
}
