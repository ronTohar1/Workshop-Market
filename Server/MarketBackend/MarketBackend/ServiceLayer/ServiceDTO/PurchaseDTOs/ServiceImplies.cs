using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    public class ServiceImplies : ServicePurchasePolicy
    {
        //condition is what is allowed only if a different things happens
        public ServicePurchasePredicate condition { get; set; }

        //allowing is the thing condition is dependent on
        public ServicePurchasePredicate allowing { get; set; }



        public ServiceImplies(ServicePurchasePredicate condition, ServicePurchasePredicate allowing, string tag="") : base(tag)
        {
            this.condition = condition;
            this.allowing = allowing;
        }
    }
}
