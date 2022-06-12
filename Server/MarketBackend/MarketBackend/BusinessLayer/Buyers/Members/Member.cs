﻿using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using System;
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
        public IList<string> pendingNotifications { get; private set; }
        private Security security; // Responsible for securing the member's details.
        private Notifier notifier;

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


        public Member(string username, string password, Security security)
        {
            //Init locks first
            this.mutex = new Mutex();
            this.loggedInLock = new ReaderWriterLock();

            //Init fields
            this.security = security;
            this.Username = username;
            this.password = security.HashPassword(password);
            this._loggedIn = false;
            this.pendingNotifications = new SynchronizedCollection<string>();
        }

        private Member(int id, string username, int password, Cart cart, 
            Security security, IList<string> pendingNotifications,
            IList<Purchase> purchaseHistory) : base(id, cart, purchaseHistory)
        {
            //Init locks first
            this.mutex = new Mutex();
            this.loggedInLock = new ReaderWriterLock();

            //Init fields
            this.security = security;
            this.Username = username;
            this.password = password;
            this._loggedIn = false;
            this.pendingNotifications = pendingNotifications;
        }

        // r S 8
        public static Member DataMemberToMember(DataMember dataMember, Security security)
        {
            Cart cart = Cart.DataCartToCart(dataMember.Cart);

            IList<string> pendingNotifications = ToSynchronized(
                dataMember.PendingNotifications
                .Select(dataNotification => dataNotification.Notification));

            IList<Purchase> purchaseHistory = ToSynchronized(
                dataMember.PurchaseHistory
                .Select(dataPurchase => Purchase.DataPurchaseToPurchase(dataPurchase)));

            return new Member(dataMember.Id, dataMember.Username, dataMember.Password,
                cart, security, pendingNotifications, purchaseHistory);
        }

        public bool Login(string password, Func<string[], bool> notifyFunc)
        {
            lock (mutex)
            {
                if (LoggedIn)
                    throw new MarketException("This user is already logged in!");
                if (this.password == security.HashPassword(password))
                {
                    this.notifier = new Notifier(notifyFunc);
                    LoggedIn = true;
                    SendPending();
                }
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

        public virtual void Notify(string[] notifications) {
            
            if (!LoggedIn || !notifier.tryToNotify(notifications))
            {
                foreach (string notification in notifications)
                    pendingNotifications.Add(notification);
            }
            
        }

        public void Notify(string notification)
       => Notify(new string[] { notification });

        private void SendPending() {
            
            if (pendingNotifications.Count > 0 && notifier.tryToNotify(pendingNotifications.ToArray()))
                pendingNotifications.Clear();
        }
        public bool matchingPasswords(string password)
        => this.password == security.HashPassword(password);

        private static SynchronizedCollection<T> ToSynchronized<T>(IEnumerable<T> elements)
        {
            SynchronizedCollection<T> synchronizedCollection = new SynchronizedCollection<T>();
            foreach (T element in elements)
            {
                synchronizedCollection.Add(element);
            }
            return synchronizedCollection;
        }

    }
}
