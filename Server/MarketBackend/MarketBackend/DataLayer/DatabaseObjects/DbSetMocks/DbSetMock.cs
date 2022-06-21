using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DatabaseObjects.DbSetMocks
{
    public class DbSetMock<T> : DbSet<T> where T : class
    {
        public override IEntityType EntityType => throw new NotImplementedException();
    }
}
