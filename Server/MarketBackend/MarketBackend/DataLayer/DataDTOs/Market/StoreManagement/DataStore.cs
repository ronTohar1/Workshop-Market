using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement
{
    public class DataStore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FounderMemberId { get; set; }
        public bool IsOpen { get; set; }
        public IList<DataProduct> Products { get; set; }
        public IList<DataPurchase> PurchaseHistory { get; set; }
        public IList<DataStoreMemberRoles> MembersPermissions { get; set; }
        public DataAppointmentsNode Appointments { get; set; }

        public DataStoreDiscountPolicyManager DiscountManager { get; set; }
        // public StorePurchasePolicyManager purchaseManager { get; set; }

        public IList<DataBid> Bids { get; set; }
    }
}
