using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers.Guests
{
    public class GuestsController : IBuyersController
    {
        private IDictionary<int, Buyer> buyers;

        private Action onEnter; 

        internal IDictionary<int, Buyer> Buyers { get { return buyers; } }

        public GuestsController()
        {
            buyers = new ConcurrentDictionary<int, Buyer>();
            onEnter = () => { };
        }

        public Buyer? GetBuyer(int buyerId)
        {
            buyers.TryGetValue(buyerId, out Buyer? buyer);
            return buyer;
        }

        public int Enter()
        {
            Buyer buyer = new();
            buyers[buyer.Id] = buyer;
            onEnter(); // does not throw an exception 
            return buyer.Id;
        }

        public void OnEnter(Action onEnter)
        {
            this.onEnter = onEnter;
        }

        public void Leave(int id) =>
            buyers.Remove(id);

    }
}
