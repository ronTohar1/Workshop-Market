
namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts
{
    internal class Discount
    {
        public int id { get; set; }
        public string description { get; set; }

        public IExpression discountExpression { get; set; }

        public Discount(int id, string description, IExpression discountExpression)
        {
            this.id = id;
            this.description = description;
            this.discountExpression = discountExpression;
        }

    }
}
