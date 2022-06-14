namespace WebAPI.Requests
{
    public class DenyBidRequest
    {
        public int StoreId { get; set; }
        public int MemberId { get; set; }
        public int BidId { get; set; }
        public DenyBidRequest(int storeId, int memberId, int bidId)
        {
            StoreId = storeId;
            MemberId = memberId;
            BidId = memberId;
        }
    }
}
