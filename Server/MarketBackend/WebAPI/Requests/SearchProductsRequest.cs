namespace WebAPI.Requests
{
    public class SearchProductsRequest
    {
        public string? StoreName { get; set; }
        public string? ProductName { get; set; }
        public string? Category { get; set; }
        public string? Keyword { get; set; }
        public int? ProductId { get; set; }
        public IList<int>? ProductIds { get; set; }
    }
}
