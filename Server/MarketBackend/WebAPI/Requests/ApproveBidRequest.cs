namespace WebAPI.Requests
{
    public class ApproveBidRequest 
    {
        public int StoreId { get; set; }
        public int MemberId { get; set; }
        public int BidId { get; set; }
        public ApproveBidRequest(int storeId, int memberId, int bidId) 
        {
            StoreId = storeId;
            MemberId = memberId;
            BidId = bidId;
        }
    }
}
