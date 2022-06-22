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
        private IDictionary<U, T> dictionary;
        private Func<T, U> getId; 

        public SimplifiedMockDbSet(Func<T, U> getId)
        {
            this.getId = getId;
            dictionary = new ConcurrentDictionary<U, T>(); 
        }

        public void AddAsync(T toAdd)
        {
            dictionary.Add(getId(toAdd), toAdd); 
        }

        public T? FindAsync(U id)
        {
            return dictionary[id]; 
        }

        public T? Remove(T toRemove)
        {
            bool removed = dictionary.Remove(getId(toRemove));
            if (removed)
                return toRemove;
            return null; 
        }

        public void RemoveRange(IList<T> toRemove)
        {
            foreach (T element in toRemove)
            {
                dictionary.Remove(getId(element)); 
            }
        }

        public IList<T> ToList()
        {
            return dictionary.Values.ToList(); 
        }
    }
}
