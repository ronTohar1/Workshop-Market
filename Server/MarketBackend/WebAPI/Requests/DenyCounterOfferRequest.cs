namespace WebAPI.Requests
{
    public class DenyCounterOfferRequest
    {
        public int StoreId { get; set; }
        public int MemberId { get; set; }
        public int BidId { get; set; }
        public DenyCounterOfferRequest(int storeId, int memberId, int bidId)
        {
            StoreId = storeId;
            MemberId = memberId;
            BidId = bidId;
        }
    }
}
