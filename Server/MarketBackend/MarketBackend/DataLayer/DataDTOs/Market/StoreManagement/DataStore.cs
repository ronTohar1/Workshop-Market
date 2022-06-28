using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement
{
    public class DataStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual DataMember? Founder { get; set; }
        public bool IsOpen { get; set; }
        public virtual IList<DataProduct?>? Products { get; set; }
        public virtual IList<DataPurchase?>? PurchaseHistory { get; set; }
        public virtual IList<DataStoreMemberRoles?>? MembersPermissions { get; set; }
        public virtual DataAppointmentsNode? Appointments { get; set; }

        public virtual DataStoreDiscountPolicyManager? DiscountManager { get; set; }
        public virtual DataStorePurchasePolicyManager? PurchaseManager { get; set; }

        public virtual IList<DataBid?>? Bids { get; set; }
        public virtual IList<DataStoreCoOwnerAppointmentApproving?>? CoOwnerAppointmentApprovings { get; set; }
    }
}
