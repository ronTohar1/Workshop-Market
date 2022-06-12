
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts
{
    public class Discount
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

        public static Discount DataDiscountToDiscount(DataDiscount dataDiscount)
        {
            return new Discount(dataDiscount.Id, dataDiscount.Description,
                IExpression.DataExpressionToIExpression(dataDiscount.DiscountExpression)); 
        }

    }
}
