using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Buyers.Members;

namespace MarketBackend.BusinessLayer.Buyers
{
    public class MembersController : IBuyersController
    {
        private readonly ConcurrentDictionary<int, Buyer> buyers;

        public MembersController()
        {
            buyers = new ConcurrentDictionary<int, Buyer>();
        }

        public Buyer GetBuyer(int buyerId)
        {
            throw new NotImplementedException();
        }

        internal Member GetMember(int memberId)
        {
            throw new NotImplementedException();
        }
    }
}
