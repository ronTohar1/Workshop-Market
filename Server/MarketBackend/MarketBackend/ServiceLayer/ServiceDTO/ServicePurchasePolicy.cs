using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServicePurchasePolicy
    {
        public int Id { get; set; }
        public string description { get; set; }

        public ServicePurchasePolicy(int id, string description)
        {
            Id = id;
            this.description = description;
        }
    }
}
