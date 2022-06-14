namespace WebAPI.Requests
{
    public class ApproveCounterOfferRequest
    {
        public int StoreId { get; set; }
        public int MemberId { get; set; }
        public int BidId { get; set; }
        public ApproveCounterOfferRequest(int storeId, int memberId, int bidId)
        {
            StoreId = storeId;
            MemberId = memberId;
            BidId = bidId;
        }
    }
}
