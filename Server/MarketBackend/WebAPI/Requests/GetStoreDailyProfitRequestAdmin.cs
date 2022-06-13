namespace WebAPI.Requests
{
    public class GetStoreDailyProfitRequestAdmin
    { 
        public int MemberId { get; set; }

        public GetStoreDailyProfitRequestAdmin(int memberId)
        {
            MemberId = memberId;
        }
    }
}
