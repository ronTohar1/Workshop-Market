using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    internal class ServiceCheckProductMore : ServiceCheckProductLess
    {
        public ServiceCheckProductMore(int productId, int amount) : base(productId, amount) { }
    }
}
