using MarketBackend.ServiceLayer.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Admins;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Buyers.Guests;
using MarketBackend.BusinessLayer.Buyers;
using SystemLog;
using NLog;
using MarketBackend.BusinessLayer.System.ExternalServices;
namespace MarketBackend.BusinessLayer
{
    public class BusiessSystemOperator
    {

        public bool marketOpen { get; private set;}
        public MembersController membersController{ get; private set;}
        public GuestsController guestsController{ get; private set;}
        public StoreController storeController{ get; private set;}
        public BuyersController buyersController { get; private set;}
        public ExternalServicesController externalServicesController{ get; private set;}
        public PurchasesManager purchasesManager{ get; private set;}
        public AdminManager adminManager{ get; private set;}
        public Logger logger{ get; private set;}

        private const string errorMsg = "Cannot give any facade when market is closed!";


        public BusiessSystemOperator()
        {
            marketOpen = false;
        }

        public int OpenMarket(string username, string password)
        {

            if (adminManager != null && !VerifyAdmin(username, password))// if adminManager isn't initialized, it's the first boot of the system 
                throw new MarketException($"User with username: {username} does not have permission to open the market!");
            if (marketOpen)
                throw new MarketException("the market is allready opened");
            if (adminManager == null)//meaning first 
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
                marketOpen = true;
                return adminId;
            }
            marketOpen = true;
            return -1;
        }

        public void CloseMarket()
        {
            if (!marketOpen)
                 throw new MarketException("Market already closed!");
            marketOpen = false;
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
            catch (Exception exception) { 
                return false;
            }
        }

    }
}
