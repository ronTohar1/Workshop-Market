using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServiceBid
    {
        public int Id { get; set; }
        public int storeId { get; set; }
        public int productId { get; set; }
        public int memberId { get; set; }
        public double bid { get; set; }
        public IList<int> approvingIds { get; }

        public bool counterOffer { get; set; }

        public ServiceBid(int bidId,int storeId, int productId, int memberId, double bid, IList<int> approvingIds, bool counterOffer)
        {
            this.storeId = storeId;
            this.productId = productId;
            this.memberId = memberId;
            this.bid = bid;
            this.approvingIds = approvingIds;
            this.counterOffer = counterOffer;
        }

        public ServiceBid(int storeId, int productId, int memberId, double bid)
        {
            this.storeId = storeId;
            this.productId = productId;
            this.memberId = memberId;
            this.bid = bid;
            approvingIds = new List<int>();
            counterOffer = false;
        }

        public override bool Equals(object? other)
        {
            if (other == null || !(other is ServiceBid))
                return false;
            ServiceBid otherBid = (ServiceBid)other;
            bool approvingIdsEqual = true;
            for(int i = 0; i < approvingIds.Count; i++)
            {
                if(approvingIds[i] != otherBid.approvingIds[i])
                    approvingIdsEqual = false;
            }
            return storeId == otherBid.storeId
                    && productId == otherBid.productId
                    && memberId == otherBid.memberId
                    && bid == otherBid.bid
                    && counterOffer == otherBid.counterOffer
                    && approvingIdsEqual;
        }

    }
}
