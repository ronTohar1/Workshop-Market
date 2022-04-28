using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers.Members
{
    public class Member: Buyer
    {
        public string Username { get; private set; }
        private int password;
        public bool LoggedIn { get; private set; }
        private IList<string> notifications;
        private Mutex mutex;

        public Member(string username,int password)
        {
            this.Username = username;
            this.password = password;
            this.LoggedIn = false;
            this.notifications = new SynchronizedCollection<string>();
            mutex = new Mutex(false);
        }

        
        public bool Login(string password)
        {
            mutex.WaitOne();
            return this.password == password.GetHashCode() && !LoggedIn;
            mutex.ReleaseMutex();
        }

        public bool Logout()
        {
            return true;
        }

        public void NotifyMember(string notification)
        {   // Do we really need this method?
            throw new NotImplementedException();
        }

        private void AddNotification(string notification)
        {
            this.notifications.Add(notification);
        }


    }
}
