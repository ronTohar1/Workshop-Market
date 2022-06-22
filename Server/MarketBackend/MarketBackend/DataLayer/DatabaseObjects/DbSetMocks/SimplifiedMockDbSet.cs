using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DatabaseObjects.DbSetMocks
{
    public class SimplifiedMockDbSet<T, U> : SimplifiedDbSet<T, U> where T : class
    {
        private Func<T> generate; 

        public SimplifiedMockDbSet(Func<T> generate)
        {
            this.generate = generate;
        }

        public void AddAsync(T toAdd)
        {
            
        }

        public T? FindAsync(U id)
        {
            return generate();
        }

        public T? Remove(T toRemove)
        {
            return toRemove; 
        }

        public void RemoveRange(IList<T> toRemove)
        {
            
        }

        public void Update(U id, Action<T> action)
        {
            
        }

        public IList<T> ToList()
        {
            return new List<T>() { generate() }; 
        }
    }
}
