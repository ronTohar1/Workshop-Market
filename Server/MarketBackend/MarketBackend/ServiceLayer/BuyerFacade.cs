using MarketBackend.BusinessLayer;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Buyers.Guests;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer.ServiceDTO;
using SystemLog;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer
{
    public class BuyerFacade : IBuyerFacade
    {
        private BuyersController buyersController;
        private MembersController membersController;
        private GuestsController guestsController;
        private StoreController storeController;
        private PurchasesManager purchasesManager;
        private Logger logger;

        public BuyerFacade(StoreController storeController, BuyersController buyersController, MembersController membersController, GuestsController guestsController, PurchasesManager purchasesManager, Logger logger)
        {
            this.buyersController = buyersController;
            this.storeController = storeController;
            this.membersController = membersController;
            this.guestsController = guestsController;
            this.purchasesManager = purchasesManager;
            this.logger = logger;
        }

        //done
        public Response<ServiceCart> GetCart(int userId)
        {
            try
            {
                Cart? c = buyersController.GetCart(userId);
                if (c == null)
                    return new Response<ServiceCart>($"No cart with user id {userId}");
                logger.Info($"GetCart was called with parameters [userId = {userId}]");
                return new Response<ServiceCart>(new ServiceCart(c));
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetCart, parameters: [userId = {userId}]");
                return new Response<ServiceCart>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetCart, parameters: [userId = {userId}]");
                return new Response<ServiceCart>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<bool> AddProdcutToCart(int userId, int storeId, int productId, int amount)
        {
            try
            {
                Cart? c = buyersController.GetCart(userId);
                if (c == null)
                    return new Response<bool>($"No cart with user id {userId}");
                // Check can buy product
                Store? store = storeController.GetOpenStore(storeId);
                if (store == null)
                    return new Response<bool>($"Trying to by from a closed store: {storeId}");

                string failMsg = store.CanBuyProduct(userId, productId, amount);
                if (failMsg != null)
                    return new Response<bool>(failMsg);

                // Can add product to cart
                c.AddProductToCart(new ProductInBag(productId, storeId), amount);
                logger.Info($"AddProdcutToCart was called with parameters [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: AddProdcutToCart, parameters: [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: AddProdcutToCart, parameters: [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<bool> RemoveProductFromCart(int userId, int storeId, int productId)
        {
            try
            {
                Cart? c = buyersController.GetCart(userId);
                if (c == null)
                    return new Response<bool>($"No cart with user id {userId}");
                c.RemoveProductFromCart(new ProductInBag(productId, storeId));
                logger.Info($"RemoveProductFromCart was called with parameters [userId = {userId}, storeId = {storeId}, productId = {productId}]");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: RemoveProductFromCart, parameters: [userId = {userId}, storeId = {storeId}, productId = {productId}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: RemoveProductFromCart, parameters: [userId = {userId}, storeId = {storeId}, productId = {productId}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<bool> changeProductAmountInCart(int userId, int storeId, int productId, int amount)
        {
            try
            {
                Cart? c = buyersController.GetCart(userId);
                if (c == null)
                    return new Response<bool>($"No cart with user id {userId}");
                c.ChangeProductAmount(new ProductInBag(productId, storeId), amount);
                logger.Info($"changeProductAmountInCart was called with parameters [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: changeProductAmountInCart, parameters: [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: changeProductAmountInCart, parameters: [userId = {userId}, storeId = {storeId}, productId = {productId}, amount = {amount}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //TODO
        public Response<ServicePurchase> PurchaseCartContent(int userId, ServicePaymentDetails paymentDetails, ServiceSupplyDetails supplyDetails)
        {
            try
            {
                Purchase purchase = purchasesManager.PurchaseCartContent(userId,
                    new PaymentDetails(paymentDetails.CardNumber, paymentDetails.Month, paymentDetails.Year, paymentDetails.Holder, paymentDetails.Ccv, paymentDetails.Id), 
                    new SupplyDetails(supplyDetails.Name, supplyDetails.Address, supplyDetails.City, supplyDetails.Country, supplyDetails.Zip));
                ServicePurchase canBuy = new ServicePurchase(purchase.purchaseDate, purchase.purchasePrice, purchase.purchaseDescription);
                logger.Info($"PurchaseCartContent was called with parameters [userId = {userId}]");
                return new Response<ServicePurchase>(canBuy);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: PurchaseCartContent, parameters: [userId = {userId}, market exception: {mex.Message}]");
                return new Response<ServicePurchase>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: PurchaseCartContent, parameters: [userId = {userId}, Exception: {ex.Message}");
                return new Response<ServicePurchase>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //TODO
        public Response<int> Enter()
        {
            try
            {
                int id = guestsController.Enter();
                logger.Info($"Enter was called");
                return new Response<int>(id);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: Enter, parameters: []");
                return new Response<int>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: Enter, parameters: []");
                return new Response<int>("Sorry, an unexpected error occured. Please try again");
            }
            return new Response<int>();
        }

        public Response<bool> Leave(int userId)
        {
            try
            {
                guestsController.Leave(userId);
                logger.Info($"Leave was called");
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: Leave, parameters: []");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: Leave, parameters: []");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<ServiceStore> GetStoreInfo(int storeId)
        {
            try
            {
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<ServiceStore>($"No store with id {storeId}");
                logger.Info($"GetStoreInfo was called with parameters [storeId = {storeId}]");
                return new Response<ServiceStore>(CreateServiceStore(s, storeId));
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetStoreInfo, parameters: [storeId = {storeId}]");
                return new Response<ServiceStore>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetStoreInfo, parameters: [storeId = {storeId}]");
                return new Response<ServiceStore>("Sorry, an unexpected error occured. Please try again");
            }
        }

        private ServiceStore CreateServiceStore(Store store, int storeId)
        {
            // try and catch is in calling functions 

            IList<int> productsIds = store.SearchProducts(new ProductsSearchFilter()).Select(product => product.id).ToList();

            return new ServiceStore(storeId, store.GetName(), productsIds);
        }

        //done
        public Response<ServiceStore> GetStoreInfo(string storeName)
        {
            try
            {
                int storeId = storeController.GetStoreIdByName(storeName);
                Store s = storeController.GetStore(storeId);
                if (s == null) // never
                    return new Response<ServiceStore>($"No store with name {storeName}");
                logger.Info($"GetStoreInfo was called with parameters [storeName = {storeName}]");
                return new Response<ServiceStore>(CreateServiceStore(s, storeId));
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetStoreInfo, parameters: [storeName = {storeName}]");
                return new Response<ServiceStore>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetStoreInfo, parameters: [storeName = {storeName}]");
                return new Response<ServiceStore>("Sorry, an unexpected error occured. Please try again");
            }
        }

        private IDictionary<int, IList<ServiceProduct>> mapToServiceMap(IDictionary<int, IList<Product>> map)
        {
            IDictionary<int, IList<ServiceProduct>> result = new Dictionary<int, IList<ServiceProduct>>();
            foreach (int key in map.Keys)
            {
                IList<Product> products = map[key];
                IList<ServiceProduct> l = new List<ServiceProduct>();
                foreach (Product product in products)
                {
                    l.Add(new ServiceProduct(product));
                }
                result[key] = l;
            }
            return result;
        }

        //done
        public Response<IDictionary<int, IList<ServiceProduct>>> ProductsSearch(string? storeName = null, string? productName = null, string? category = null, string? keyword = null, int? productId = null, IList<int> productIds = null, ServiceMemberInRole memberInRole = null, bool storesWithProductsThatPassedFilter = true)
        {
            try
            {
                ProductsSearchFilter filter = new ProductsSearchFilter();
                if (storeName != null)
                    filter.FilterStoreName(storeName);
                if (productName != null)
                    filter.FilterProductName(productName);
                if (category != null)
                    filter.FilterProductCategory(category);
                if (keyword != null)
                    filter.FilterProductKeyword(keyword);
                if (productId != null)
                    filter.FilterProductId((int)productId); // right after check that is not null
                if (productIds != null)
                    filter.FilterProductIds(productIds);
                if (memberInRole != null)
                    filter.FilterStoreOfMemberInRole(memberInRole.MemberId, memberInRole.RoleInStore);
                IDictionary<int, IList<Product>> prods = storeController.SearchProductsInOpenStores(filter, storesWithProductsThatPassedFilter); 
                logger.Info($"ProductsSearch was called with parameters [storeName = {storeName}, productName = {productName}, category = {category}, keyword = {keyword}, productId = {productId}, is productIds null ? = {productIds == null}, is memberInRole null ? = {memberInRole == null}, storesWithProductsThatPassedFilter: {storesWithProductsThatPassedFilter}]");
                return new Response<IDictionary<int, IList<ServiceProduct>>>(mapToServiceMap(prods));
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: ProductsSearch, parameters: [storeName = {storeName}, productName = {productName}, category = {category}, keyword = {keyword}, productId = {productId}, is productIds null ? = {productIds == null}, is memberInRole null ? = {memberInRole == null}, storesWithProductsThatPassedFilter: {storesWithProductsThatPassedFilter}]");
                return new Response<IDictionary<int, IList<ServiceProduct>>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: ProductsSearch, parameters: [storeName = {storeName}, productName = {productName}, category = {category}, keyword = {keyword}, productId = {productId}, is productIds null ? = {productIds == null}, is memberInRole null ? = {memberInRole == null}, storesWithProductsThatPassedFilter: {storesWithProductsThatPassedFilter}]");
                return new Response<IDictionary<int, IList<ServiceProduct>>>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<int> Register(string userName, string password)
        {
            try
            {
                int id = membersController.Register(userName, password);
                logger.Info($"Register was called with parameters [userName = {userName}, password undisclosed]");
                return new Response<int>(id);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: Register, parameters: [userName = {userName}, password undisclosed]");
                return new Response<int>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: Register, parameters: [userName = {userName}, password undisclosed]");
                return new Response<int>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<int> Login(string userName, string password, Func<string[], bool> notifier)
        {
            try
            {
                Member? m = membersController.GetMember(userName);
                if (m == null)
                    return new Response<int>($"No member with userName {userName}");

                bool logged = m.Login(password, notifier);// the member could have connected from another computer 
                if (logged == false)
                    return new Response<int>("Incorrect password");
                int id = m.Id;
                logger.Info($"Login was called with parameters [userName = {userName}, password undisclosed]");
                return new Response<int>(id);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: Login, parameters: [userName = {userName}, password undisclosed]");
                return new Response<int>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: Login, parameters: [userName = {userName}, password undisclosed]");
                return new Response<int>("Sorry, an unexpected error occured. Please try again");
            }
        }

        // private Func<string[], bool> produceNotifierFunc()
        // {
        //     //this function will recieve the communication means and will return
        //     //a closure, that given string[] to transfer will attempt to send to 
        //     //the client the array - David on it, waiting for communication means tho

        //     Func<string[], bool> tryToSend = (string[] messages) =>
        //     {
        //         //bool succeddded = Socket.send(messages);
        //         //return succeddded;
        //         throw new NotImplementedException();
        //     };

        //     return tryToSend;

        // }

        //done
        public Response<bool> Logout(int memberId)
        {
            try
            {
                Member? m = membersController.GetMember(memberId);
                if (m == null)
                    return new Response<bool>($"No member with the id {memberId}");
                logger.Info($"Logout was called with parameters [memberId = {memberId}]");
                m.Logout();
                return new Response<bool>(true);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: Logout, parameters: [memberId = {memberId}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: Logout, parameters: [memberId = {memberId}]");
                return new Response<bool>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //TODO
        public Response<bool> AddProductReview(int memberId, int storeId, int productId, string review)
        {
            try
            {
                logger.Info($"addproductreview was called with parameters [memberid = {memberId}, storeid = {storeId}, productid = {productId}, review = {review}]");
                Store s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<bool>($"No store with id {storeId}");
                s.AddProductReview(memberId, productId, review);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: addproductreview, parameters: [memberid = {memberId}, storeid = {storeId}, productid = {productId}, review = {review}]");
                return new Response<bool>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: addproductreview, parameters: [memberid = {memberId}, storeid = {storeId}, productid = {productId}, review = {review}]");
                return new Response<bool>("sorry, an unexpected error occured. please try again");
            }
            return new Response<bool>();
        }

        public Response<IDictionary<int, string>> GetDiscountsDescriptions(int storeId)
        {
            try
            {
                Store? s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<IDictionary<int, string>>($"There isn't a store with an id {storeId}");
                IDictionary<int, string> discounts = s.GetDiscountPolicyDescriptions();
                logger.Info($"GetDiscountsDescriptions was called with parameters: [storeId = {storeId}]");
                return new Response<IDictionary<int, string>>(discounts);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetDiscountsDescriptions, parameters: [storeId = {storeId}]");
                return new Response<IDictionary<int, string>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetDiscountsDescriptions, parameters: [storeId = {storeId}]");
                return new Response<IDictionary<int, string>>("Sorry, an unexpected error occured. Please try again");
            }
        }

        public Response<IDictionary<int, string>> GetPurchasePolicyDescriptions(int storeId)
        {
            try
            {
                Store? s = storeController.GetStore(storeId);
                if (s == null)
                    return new Response<IDictionary<int, string>>($"There isn't a store with an id {storeId}");
                IDictionary<int, string> policies = s.GetPurchasePolicyDescriptions();
                logger.Info($"GetPurchasePolicyDescriptions was called with parameters: [storeId = {storeId}]");
                return new Response<IDictionary<int, string>>(policies);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: GetPurchasePolicyDescriptions, parameters: [storeId = {storeId}]");
                return new Response<IDictionary<int, string>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: GetPurchasePolicyDescriptions, parameters: [storeId = {storeId}]");
                return new Response<IDictionary<int, string>>("Sorry, an unexpected error occured. Please try again");
            }
        }
    }
}
