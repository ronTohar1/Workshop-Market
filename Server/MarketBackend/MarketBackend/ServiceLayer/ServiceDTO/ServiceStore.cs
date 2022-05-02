using MarketBackend.BusinessLayer.Market.StoreManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    internal class ServiceStore
    {
        public string Name { get; }
        public IList<int> ProductsIds { get; }

        public ServiceStore(string name, IList<int> productsIds)
        {
            Name = name;
            ProductsIds = productsIds;
        }
    }
}
