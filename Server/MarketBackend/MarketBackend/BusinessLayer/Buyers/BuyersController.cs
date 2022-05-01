using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Buyers.Guests;

namespace MarketBackend.BusinessLayer.Buyers
{
    public class BuyersController
    {
        private readonly IList<IBuyersController> buyersControllers;

        public BuyersController()
        {
            buyersControllers = new List<IBuyersController>();
            buyersControllers.Add(new MembersController());
            buyersControllers.Add(new GuestsController());
        }

        public bool BuyerAvailable(int buyerId) => 
            buyersControllers.Any(controller => controller.GetBuyer(buyerId) is not null);

        public virtual Buyer? GetBuyer(int buyerId)
        {
            foreach (var controller in buyersControllers)
            {
                Buyer? buyer = controller.GetBuyer(buyerId);
                if (buyer is not null)
                    return buyer;
            }
            return null;
        }

        public Cart? GetCart(int buyerId)
        {
            Buyer? buyer = GetBuyer(buyerId);
            if (buyer is not null)
                return buyer.Cart;
            else
                return null;
        }

       



    }
}
