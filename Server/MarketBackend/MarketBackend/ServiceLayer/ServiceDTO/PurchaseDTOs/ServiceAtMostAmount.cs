using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    public class ServiceAtMostAmount : ServiceAtlestAmount
    {
        public ServiceAtMostAmount(int productId, int amount, string tag = "") : base(productId, amount, tag) { }
    }
}
