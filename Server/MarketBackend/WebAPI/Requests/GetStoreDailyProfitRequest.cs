namespace WebAPI.Requests
{
    public class GetStoreDailyProfitRequest
    {
        public int StoreId { get; set; }
        public int MemberId { get; set; }

        public GetStoreDailyProfitRequest(int storeId, int memberId)
        {
            StoreId = storeId;
            MemberId = memberId;
        }
    }
}
