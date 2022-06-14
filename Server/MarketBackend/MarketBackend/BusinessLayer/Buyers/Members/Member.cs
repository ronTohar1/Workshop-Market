using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataManagers;
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

        //r S 8
        public virtual void Notify(string[] notifications) {
            
            if (!LoggedIn || !notifier.tryToNotify(notifications))
            {
                foreach (string notification in notifications)
                {
                    DataMember dm = MemberDataManager.GetInstance().Find(Id);
                    dm.PendingNotifications.Add(new DataNotification()
                    {
                        Notification = notification
                    });
                    MemberDataManager.GetInstance().Save();
                    pendingNotifications.Add(notification);
                }
            }
            
        }

        public virtual void NotifyNoSave(string[] notifications)
        {

            if (!LoggedIn || !notifier.tryToNotify(notifications))
            {
                foreach (string notification in notifications)
                {
                    pendingNotifications.Add(notification);
                }
            }

        }

        public virtual void DataNotify(string[] notifications)
        {
            MemberDataManager memberDataManager = MemberDataManager.GetInstance();
            DataMember dataMember = memberDataManager.Find(Id); 
            if (!LoggedIn)
            {
                foreach (string notification in notifications)
                {
                    dataMember.PendingNotifications.Add(new DataNotification() { Notification = notification });
                }
            }
        }

        public void Notify(string notification)
       => Notify(new string[] { notification });

        public void NotifyNoSave(string notification)
       => NotifyNoSave(new string[] { notification });

        public void DataNotify(string notification)
       => DataNotify(new string[] { notification });

        //r S 8
        private void SendPending() {
            if (pendingNotifications.Count > 0 && notifier.tryToNotify(pendingNotifications.ToArray()))
            {
                DataMember dm = MemberDataManager.GetInstance().Find(Id);
                dm.PendingNotifications.Clear();
                MemberDataManager.GetInstance().Save();
                pendingNotifications.Clear();
            }
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

        public override void ChangeProductAmount(ProductInBag product, int amount, int memberId)
        {
            int storeId = product.StoreId;

            DataMember dm = MemberDataManager.GetInstance().Find(memberId);
            DataProductInBag? dpib = FindDataProductInBag(dm, storeId, product.ProductId);

            Cart.ShoppingBags[storeId].ChangeProductAmount(product, amount, dpib);
        }

        // r S 8 - database functions
        public DataMember MemberToDataMember()
        {
            DataMember dMember = new DataMember()
            {
                Id = Id,
                Username = Username,
                Password = password,
                Cart = Cart.CartToDataCart(),
                IsAdmin = false
            };
            return dMember;
        }

        public void RemoveCartFromDB(DataMember member)
        {
            if (member == null) return;
            DataCart? c = member.Cart;
            if (c != null)
            {
                Cart.RemoveContentFromDB(c);
                CartDataManager.GetInstance().Remove(c.Id);
            }
        }

        public DataProductInBag? FindDataProductInBag(DataMember dm, int storeId, int productId)
        {
            if (dm == null) return null;
            DataCart? dc = dm.Cart;
            if (dc == null)
                return null;
            foreach (DataShoppingBag dsb in dc.ShoppingBags)
            {
                if (dsb.Store.Id == storeId)
                {
                    foreach (DataProductInBag dpib in dsb.ProductsAmounts)
                    {
                        if (dpib.ProductId == productId)
                            return dpib;
                    }
                }
            }
            return null;
        }            
    }
}
