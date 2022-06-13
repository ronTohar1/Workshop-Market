namespace WebAPI.Requests
{
    public class GetApproveForBidRequest
    {
        public int StoreId { get; set; }
        public int MemberId { get; set; }
        public int BidId { get; set; }
        public GetApproveForBidRequest(int storeId, int memberId, int bidId)
        {
            StoreId = storeId;
            MemberId = memberId;
            BidId = bidId;
        }
    }
}
