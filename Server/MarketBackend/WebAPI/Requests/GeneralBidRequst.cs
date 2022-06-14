namespace WebAPI.Requests
{
    public class GeneralBidRequst
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int MemberId { get; set; }

        public GeneralBidRequst(int storeId, int prouctId, int memberId) {
            StoreId=storeId;
            ProductId=prouctId;
            MemberId=memberId;
        }
    }
}
