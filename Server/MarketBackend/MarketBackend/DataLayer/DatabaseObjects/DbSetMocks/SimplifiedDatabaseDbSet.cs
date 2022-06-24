using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DatabaseObjects.DbSetMocks
{
    public class SimplifiedDatabaseDbSet<T, U> : SimplifiedDbSet<T, U> where T : class
    {
        private DbSet<T> dbSet;
        private DbContext db;
        private Func<T, U> getId;
        public SimplifiedDatabaseDbSet(DbSet<T> dbSet, DbContext db, Func<T, U> getId)
        {
            this.dbSet = dbSet;
            this.db = db;
            this.getId = getId;
        }
        public void AddAsync(T toAdd)
        {
            dbSet.AddAsync(toAdd);
        }

        public T? FindAsync(U id)
        {
            T elementOfId = dbSet.FindAsync(id).Result;
            return IncludeProperties().FirstOrDefault(element => element == elementOfId);
        }

        public T? Remove(T toRemove)
        {
            return dbSet.Remove(toRemove).Entity;
        }

        public void RemoveRange(IList<T> toRemove)
        {
            dbSet.RemoveRange(toRemove);
        }

        public void Update(U id, Action<T> action)
        {
            T? dataObject = FindAsync(id);
            if (dataObject == null)
                throw new Exception("Object of id: " + id + " is not in the database");
            action(dataObject);
        }

        public IList<T> ToList()
        {
            return IncludeProperties().ToList();
        }

        private IQueryable<T> IncludeProperties()
        {
            var entity = db.Model.GetEntityTypes().FirstOrDefault(entity => entity.ClrType == typeof(T));

            IQueryable<T> q = dbSet;

            IList<string> navigationPropertiesStrings = GetNavigationPropertiesStrings(entity, new List<IEntityType>() { entity });
            return navigationPropertiesStrings.Aggregate(q, (c, s) => c.Include(s));
        }

        private IList<string> GetNavigationPropertiesStrings(IEntityType entity, IList<IEntityType> inNavigationStrings) // for cycles 
        {
            var navigationProperties = entity.GetNavigations().Where(property => !inNavigationStrings.Contains(property.TargetEntityType)).ToList();
           
            IList<string> firstNavigation = navigationProperties.Select(property => property.Name).ToList();
            inNavigationStrings = inNavigationStrings.Concat(navigationProperties.Select(property => property.TargetEntityType)).ToList(); 
            
            IList<string> secondOrMoreNavigations = navigationProperties.SelectMany(
                property => GetNavigationPropertiesStrings(property.TargetEntityType, inNavigationStrings)
                .Select(navigationString => property.Name + "." + navigationString)).ToList();
            return firstNavigation.Concat(secondOrMoreNavigations).ToList(); 
        }
    }
}
