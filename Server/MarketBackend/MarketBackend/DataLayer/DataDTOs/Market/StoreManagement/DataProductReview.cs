using MarketBackend.DataLayer.DataDTOs.Buyers;

namespace MarketBackend.DataLayer.DataDTOs
{
    public class DataProductReview
    {
        public int Id { get; set; }
        public DataMember Member { get; set; }
        public DataProduct Product { get; set; }
        public string Review { get; set; }
    }
}