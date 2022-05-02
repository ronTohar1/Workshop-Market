using MarketBackend.BusinessLayer;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Buyers.Guests;
using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer.ServiceDTO;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer
{
    internal class BuyerFacade
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
        public Response<ServicePurchaseAttempt> PurchaseCartContent(int userId, IDictionary<int, IList<Tuple<int, int>>> producstToBuyByStoresIds)
        {
            try
            {
                ServicePurchaseAttempt canBuy = new ServicePurchaseAttempt(purchasesManager.PurchaseCartContent(userId, producstToBuyByStoresIds));
                logger.Info($"PurchaseCartContent was called with parameters [userId = {userId}, producstToBuyByStoresIds = {producstToBuyByStoresIds}]");
                return new Response<ServicePurchaseAttempt>(canBuy);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: , parameters: [userId = {userId}, producstToBuyByStoresIds = {producstToBuyByStoresIds}]");
                return new Response<ServicePurchaseAttempt>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: , parameters: [userId = {userId}, producstToBuyByStoresIds = {producstToBuyByStoresIds}]");
                return new Response<ServicePurchaseAttempt>("Sorry, an unexpected error occured. Please try again");
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
                return new Response<ServiceStore>(CreateServiceStore(s));
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

        private ServiceStore CreateServiceStore(Store store)
        {
            // try and catch is in calling functions 

            IList<int> productsIds = store.SearchProducts(new ProductsSearchFilter()).Select(product => product.id).ToList();

            return new ServiceStore(store.GetName(), productsIds);
        }

        //done
        public Response<ServiceStore> GetStoreInfo(string storeName)
        {
            try
            {
                Store s = storeController.GetStore(storeController.GetStoreIdByName(storeName));
                if (s == null) // never
                    return new Response<ServiceStore>($"No store with name {storeName}");
                logger.Info($"GetStoreInfo was called with parameters [storeName = {storeName}]");
                return new Response<ServiceStore>(CreateServiceStore(s));
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
        public Response<IDictionary<int, IList<ServiceProduct>>> ProductsSearch(string? storeName = null, string? productName = null, string? category = null, string? keyword = null)
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
                IDictionary<int, IList<Product>> prods = storeController.SearchProductsInOpenStores(filter);
                logger.Info($"ProductsSearch was called with parameters [storeName = {storeName}, productName = {productName}, category = {category}, keyword = {keyword}]");
                return new Response<IDictionary<int, IList<ServiceProduct>>>(mapToServiceMap(prods));
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: ProductsSearch, parameters: [storeName = {storeName}, productName = {productName}, category = {category}, keyword = {keyword}]");
                return new Response<IDictionary<int, IList<ServiceProduct>>>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: ProductsSearch, parameters: [storeName = {storeName}, productName = {productName}, category = {category}, keyword = {keyword}]");
                return new Response<IDictionary<int, IList<ServiceProduct>>>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<int> Register(string userName, string password)
        {
            try
            {
                int id = membersController.Register(userName, password);
                logger.Info($"Register was called with parameters [userName = {userName}, password = {password}]");
                return new Response<int>(id);
            }
            catch (MarketException mex)
            {
                logger.Error(mex, $"method: Register, parameters: [userName = {userName}, password = {password}]");
                return new Response<int>(mex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"method: Register, parameters: [userName = {userName}, password = {password}]");
                return new Response<int>("Sorry, an unexpected error occured. Please try again");
            }
        }

        //done
        public Response<int> Login(string userName, string password)
        {
            try
            {
                Member? m = membersController.GetMember(userName);
                if (m == null)
                    return new Response<int>($"No member with userName {userName}");
                bool logged = m.Login(password);
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

    }
}
