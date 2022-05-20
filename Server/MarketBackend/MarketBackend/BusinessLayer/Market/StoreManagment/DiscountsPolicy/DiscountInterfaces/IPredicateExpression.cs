
using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces
{
 
    public interface IPredicateExpression
    {
        public bool EvaluatePredicate(ShoppingBag bag, Store store);
    }
}
