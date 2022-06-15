using MarketBackend.BusinessLayer.Admins;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Buyers.Guests;
using MarketBackend.BusinessLayer.Buyers;
using SystemLog;
using NLog;
using MarketBackend.BusinessLayer.System.ExternalServices;
using MarketBackend.DataLayer.DataManagers;

namespace MarketBackend.BusinessLayer
{
    public class BusiessSystemOperator
    {

        public bool MarketOpen { get; private set;}
        public int MarketOpenerAdminId { get; private set; }

        public MembersController membersController{ get; private set;}
        public GuestsController guestsController{ get; private set;}
        public StoreController storeController{ get; private set;}
        public BuyersController buyersController { get; private set;}
        public ExternalServicesController externalServicesController{ get; private set;}
        public PurchasesManager purchasesManager{ get; private set;}
        public AdminManager adminManager{ get; private set;}
        public Logger logger{ get; private set;}

        public BusiessSystemOperator(string username, string password, bool loadDatabase=true)
        {
            MarketOpenerAdminId = -1;
            if (loadDatabase)
                OpenMarketWithDatabase(username, password);
            else
                OpenMarket(username, password);
        }

        public void OpenMarketWithDatabase(string username, string password)
        {
            if (adminManager != null && !VerifyAdmin(username, password))   // if adminManager isn't initialized, it's the first boot of the system 
                throw new MarketException($"User with username: {username} does not have permission to open the market!");
            if (MarketOpen)
                throw new MarketException("the market is already opened");
            if (adminManager == null)   //meaning first 
            {
                InitLogger();
                membersController = MembersController.LoadMembersController();
                // int adminId = membersController.Register(username, password);
                //Init controllers
                guestsController = new();
                storeController = StoreController.LoadStoreController(membersController);
                buyersController = new(new List<IBuyersController> { guestsController, membersController });
                HttpClient httpClient = new HttpClient();
                externalServicesController = new(new ExternalPaymentSystem(httpClient), new ExternalSupplySystem(httpClient));

                purchasesManager = new(storeController, buyersController, externalServicesController);

                adminManager = AdminManager.LoadAdminManager(storeController, buyersController, membersController);
                // adminManager.AddAdmin(adminId);
                MarketOpen = true;
                MarketOpenerAdminId = 1; // todo: change this 
            }
            MarketOpen = true;
        }

        public void OpenMarket(string username, string password)
        {
            if (adminManager != null && !VerifyAdmin(username, password))   // if adminManager isn't initialized, it's the first boot of the system 
                throw new MarketException($"User with username: {username} does not have permission to open the market!");
            if (MarketOpen)
                throw new MarketException("the market is already opened");
            if (adminManager == null)   //meaning first 
            {
                InitLogger();
                membersController = new();
                int adminId = membersController.Register(username, password);
                //Init controllers
                guestsController = new();
                storeController = new(membersController);
                buyersController = new(new List<IBuyersController> { guestsController, membersController });
                HttpClient httpClient = new HttpClient();
                externalServicesController = new(new ExternalPaymentSystem(httpClient), new ExternalSupplySystem(httpClient));

                purchasesManager = new(storeController, buyersController, externalServicesController);

                adminManager = new(storeController, buyersController, membersController);
                adminManager.AddAdmin(adminId);
                MarketOpen = true;
                MarketOpenerAdminId = adminId;
            }
            MarketOpen = true;
        }

        public void CloseMarket(bool clearDatabase=false)
        {
            if (!MarketOpen)
                 throw new MarketException("Market already closed!");
            MarketOpen = false;
            MarketOpenerAdminId = -1;

            if (clearDatabase)
            {
                throw new NotImplementedException(); //TODO: clear database
            }

            // getting rid of the business controllers
            membersController = null;
            guestsController = null;
            storeController = null;
            buyersController = null;
            adminManager = null;
            externalServicesController = null;
            purchasesManager = null;
            adminManager = null;
        }

        private void InitLogger()
        {
            logger = SystemLogger.getLogger();
        }

        private bool VerifyAdmin(string username, string password)
        {
            try
            {
                Member member = membersController.GetMember(username);
                if (!member.matchingPasswords(password))
                    return false;
                return adminManager.ContainAdmin(member.Id);
            }
            catch (Exception)
            { 
                return false;
            }
        }

        internal static void RemoveAllDatabaseContent()
        {
            StoreDataManager.GetInstance().RemoveAllTables();
        }
    }
}
