using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy
{
    public abstract class DataExpression
    {
        public int Id { get; set; }
    }
}
