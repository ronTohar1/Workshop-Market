using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    public class ServiceExpression
    {
        public string tag { get; set ; }

        public ServiceExpression(string tag = "") {
            this.tag = tag;
        }
    }
}
