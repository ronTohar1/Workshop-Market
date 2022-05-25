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
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public int AvailableQuantity { get; set; }

        public int Store { get; set; }
        public ServiceProduct(Product p, int storeId)
        {
            Id = p.id; 
            Name = p.name;
            Price = p.GetPrice();
            Category = p.category;
            AvailableQuantity = p.amountInInventory;
            Store = storeId;
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
