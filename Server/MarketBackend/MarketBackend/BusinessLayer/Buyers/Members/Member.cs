﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers.Members
{
    public class Member : Buyer
    {
        public string Username { get; private set; }
        private int password;
        private bool _loggedIn;
        private IList<string> notifications;
        private Security security; // Responsible for securing the member's details.
        /*private Notifier notifier;*/

        private Mutex mutex;
        private ReaderWriterLock loggedInLock;
        private const int lockTime = 2000;

        public bool LoggedIn
        {
            get
            {   
                loggedInLock.AcquireReaderLock(lockTime);
                try { return _loggedIn; }
                finally { loggedInLock.ReleaseReaderLock(); }
            }
            private set
            {
                loggedInLock.AcquireWriterLock(lockTime);
                try { _loggedIn = value; }
                finally { loggedInLock.ReleaseWriterLock(); }

            }
        }


        public Member(string username, string password)
        {
            //Init locks first
            this.mutex = new Mutex();
            this.loggedInLock = new ReaderWriterLock();

            //Init fields
            this.security = new Security();
            this.Username = username;
            this.password = security.HashPassword(password);
            this._loggedIn = false;
            this.notifications = new SynchronizedCollection<string>();

        }


        /* Login with notifications
         * public bool Login(string password,Notifier notifier)
        {
            lock (mutex)
            {
                if (LoggedIn)
                    throw new MarketException("Cannot login to a user that is logged in!");
                LoggedIn = true;
                this.notifier = notifier;
                return this.password == password.GetHashCode();
            }
        }*/

        public bool Login(string password)
        {
            lock (mutex)
            {
                if (LoggedIn)
                    throw new MarketException("This user is already logged in!");
                if (this.password == security.HashPassword(password))
                    LoggedIn = true;
                return LoggedIn;
            }
        }

        public void Logout()
        {
            lock (mutex)
            {
                if (!LoggedIn)
                    throw new Exception("Called Member Logout() when member was logged in!");
                LoggedIn = false;
            }
        }


    }
}
