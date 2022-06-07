using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataManagementObjects
{
    public class ProductDataManager : ObjectDataManager<DataProduct, int>
    {
        public ProductDataManager(Database db) : base(db)
        {
        }

        protected override void AddThrows(DataProduct toAdd)
        {
            db.AddAsync(toAdd); 
        }

        protected override DataProduct FindThrows(int id)
        {
            throw new NotImplementedException();
        }

        protected override IList<DataProduct> FindThrows(Predicate<DataProduct> predicate)
        {
            throw new NotImplementedException();
        }

        protected override DataProduct RemoveThrows(DataProduct toRemove)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateThrows(int id, Action<DataProduct> action)
        {
            throw new NotImplementedException();
        }
    }
}
