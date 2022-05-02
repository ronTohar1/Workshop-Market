using MarketBackend.BusinessLayer.Market.StoreManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class ServiceProduct
    {
        public int Id { get; }
        public ServiceProduct(Product p)
        {
            Id = p.id; 
        }

        public override bool Equals(Object? other)
        {
            if (other == null || !(other is ServiceProduct))
                return false;
            return this.Id == ((ServiceProduct)other).Id;
        }

        public override int GetHashCode()
        {
            return Id; // todo: make sure this is okay
        }
    }
}
