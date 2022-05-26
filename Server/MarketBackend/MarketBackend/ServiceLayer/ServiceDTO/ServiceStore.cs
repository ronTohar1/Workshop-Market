using MarketBackend.BusinessLayer.Market.StoreManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServiceStore
    {
        public int Id { get; }
        public string Name { get; }
        public IList<ServiceProduct> Products { get; private set; }

        public ServiceStore(int id, string name, IList<ServiceProduct> productsIds)
        {
            Id = id;
            Name = name;
            Products = productsIds;
        }

        public override bool Equals(Object? other)
        {
            if (other == null || !(other is ServiceStore))
                return false;
            ServiceStore otherStore = (ServiceStore)other;
            if (otherStore.Products == null)
                return false;
            return this.Id == otherStore.Id && this.Name.Equals(otherStore.Name) && SameElements(this.Products, otherStore.Products);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Products.GetHashCode()); // todo: make sure this is okay
        }

        private bool SameElements<T>(IList<T> list1, IList<T> list2)
        {
            if (list1.Count != list2.Count)
                return false;
            return list1.All(element => list2.Contains(element));

        }
    }
}
