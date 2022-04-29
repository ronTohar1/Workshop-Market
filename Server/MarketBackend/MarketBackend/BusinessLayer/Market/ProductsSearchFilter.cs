using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market
{
    public class ProductsSearchFilter
    {
        // r 2.2 (this class)

        // store search data
        private string storeName { get; set; }

        // product search data
        private string productName { get; set; }
        private string productCateory { get; set; }
        private string productKeyword { get; set; }

        // search data null means no filter by the field

        public ProductsSearchFilter()
        {
            // defualts are null
        }
    }
}
