using MarketBackend.BusinessLayer;
using MarketBackend.DataLayer.DatabaseObjects;
using MarketBackend.DataLayer.DataDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataManagers
{
    public abstract class ObjectDataManager<T, U>
    {

        // todo: implement with async
        // todo: check what async means 
        // todo: when delivering objects to business layer check maybe need to cache so there will be one instance of each 
        // todo: implement tests 

        protected Database db;

        public ObjectDataManager()
        {
            db = Database.GetInstance();
        }

        public void Add(T toAdd)
        {
            TryAction(() => AddThrows(toAdd));
        }

        protected abstract void AddThrows(T toAdd);

        public T Find(U id)
        {
            return TryFunction(() => FindThrows(id));
        }
        protected abstract T FindThrows(U id);

        public IList<T> Find(Predicate<T> predicate)
        {
            return TryFunction(() => FindThrows(predicate));
        }

        protected abstract IList<T> FindThrows(Predicate<T> predicate);

        public void Update(U id, Action<T> action)
        {
            TryAction(() => UpdateThrows(id, action));
        }

        protected void UpdateThrows(U id, Action<T> action)
        {
            action(FindThrows(id));
        }

        public T Remove(U id)
        {
            return TryFunction(() => RemoveThrows(id));
        }

        protected T RemoveThrows(U id)
        {
            return RemoveThrows(FindThrows(id));
        }

        protected abstract T RemoveThrows(T toRemove);

        public void Save()
        {
            TryAction(() => SaveThrows());
        }

        protected void SaveThrows()
        {
            db.SaveChanges();
        }

        private void TryAction(Action action)
        {
            TryFunction(() => { action(); return -1; });
        }

        private V TryFunction<V>(Func<V> function)
        {
            try
            {
                return function();
            }
            catch (Exception exception) // todo: check if this is the exception that is needed to be catch here
            {
                throw new MarketException("Action could not be saved, try again later");
            }
        }
    }
}
