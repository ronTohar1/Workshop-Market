namespace WebAPI.Requests
{
    public class GetDailyVisitoresRequestAdmin
    {
        public int MemberId { get; set; }
        public int FromDay { get; set; }
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public int ToDay { get; set; }
        public int ToMonth { get; set; }
        public int ToYear { get; set; }

        public GetDailyVisitoresRequestAdmin(int memberId, int fromDay, int fromMonth, int fromYear, int toDay, int toMonth, int toYear)
        {
            MemberId = memberId;
            FromDay = fromDay;
            FromMonth = fromMonth;
            FromYear = fromYear;
            ToDay = toDay;
            ToMonth = toMonth;
            ToYear = toYear;
        }
    }
}
