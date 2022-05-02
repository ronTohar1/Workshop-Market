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

        public override bool Equals(Object? other)
        {
            if (other == null || !(other is ServiceStore))
                return false;
            ServiceStore otherStore = (ServiceStore)other;
            if (otherStore.ProductsIds == null)
                return false;
            return this.Name.Equals(otherStore.Name) && SameElements(this.ProductsIds, otherStore.ProductsIds);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ProductsIds.GetHashCode()); // todo: make sure this is okay
        }

        private bool SameElements<T>(IList<T> list1, IList<T> list2)
        {
            if (list1.Count != list2.Count)
                return false;
            return list1.All(element => list2.Contains(element));

        }
    }
}
