using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces
{
    public interface IPurchasePolicy
    {
        // return true if there is no problem, and false if the purchse cant be made.
        public bool IsSatisfied(ShoppingBag bag);
    }
}
