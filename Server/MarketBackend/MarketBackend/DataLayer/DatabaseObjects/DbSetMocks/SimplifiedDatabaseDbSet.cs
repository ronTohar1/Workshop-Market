using Microsoft.EntityFrameworkCore;
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
        public SimplifiedDatabaseDbSet(DbSet<T> dbSet)
        {
            this.dbSet = dbSet;
        }
        public void AddAsync(T toAdd)
        {
            dbSet.AddAsync(toAdd);
        }

        public T? FindAsync(U id)
        {
            return dbSet.FindAsync(id).Result; 
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
            T dataObject = dbSet.Find(id);
            if (dataObject == null)
                throw new Exception("Object of id: " + id + " is not in the database"); 
            action(dataObject);
        }

        public IList<T> ToList()
        {
            return dbSet.ToList(); 
        }
    }
}
