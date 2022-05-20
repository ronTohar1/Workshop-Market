using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    internal class ServiceImplies : IServicePurchase
    {
        //condition is what is allowed only if a different things happens
        public IServicePredicate condition { get; set; }

        //allowing is the thing condition is dependent on
        public IServicePredicate allowing { get; set; }



        public ServiceImplies(IServicePredicate condition, IServicePredicate allowing)
        {
            this.condition = condition;
            this.allowing = allowing;
        }
    }
}
