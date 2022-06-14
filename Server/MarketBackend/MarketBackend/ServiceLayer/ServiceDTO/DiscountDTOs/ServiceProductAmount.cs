using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    public class ServiceProductAmount : ServicePredicate
    {
        public int pid { get; set; }
        public int quantity { get; set; }

        public ServiceProductAmount(int pid, int quantity, string tag = "") : base(tag) 
        {
            this.pid = pid;
            this.quantity = quantity;
        }
    }
}
