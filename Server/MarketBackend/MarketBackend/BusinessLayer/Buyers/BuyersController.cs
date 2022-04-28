using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers.Guests
{
    internal class BuyersController
    {
        private readonly IList<IBuyersController> buyersControllers;
        public BuyersController()
        {
            this.buyersControllers = new List<IBuyersController>();
            this.buyersControllers.Add(new MembersController());
            this.buyersControllers.Add(new GuestController());
        }

    }
}
