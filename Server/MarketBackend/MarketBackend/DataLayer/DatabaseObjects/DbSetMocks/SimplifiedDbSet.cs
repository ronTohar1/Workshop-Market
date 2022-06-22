using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DatabaseObjects
{
    public interface SimplifiedDbSet<T, U> where T : class
    {
        public void AddAsync(T toAdd);
        public T? FindAsync(U id);
        public T? Remove(T toRemove);
        public void RemoveRange(IList<T> toRemove);
        void Update(U id, Action<T> action);
        public IList<T> ToList();
    }
}
