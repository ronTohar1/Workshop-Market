using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer;
using MarketBackend.ServiceLayer.ServiceDTO;
using MarketBackend.SystemSettings;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Requests;
using WebSocketSharp.Server;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : Controller
    {
        private readonly IBuyerFacade buyerFacade;
        private readonly IAdminFacade adminFacade;
        private readonly IStoreManagementFacade storeManagementFacade;
        private WebSocketServer notificationServer;
        private WebSocketServer logsServer;
        private string logsPath = "/logs";
        private static int adminId = -1;
        private static IDictionary<int, string> buyerIdToRelativeNotificationPath = new Dictionary<int, string>();
        private static IDictionary<string, IList<string>> buyerUnsentMessages = new Dictionary<string, IList<string>>();
        private class NotificationsService : WebSocketBehavior
        {

        }

        private class logsService : WebSocketBehavior
        {

        }

        public BuyersController(IBuyerFacade buyerFacade, IAdminFacade adminFacade, IStoreManagementFacade storeManagementFacade, WebSocketServer notificationServer, WebSocketServer logsServer)
        {
            this.buyerFacade = buyerFacade;
            this.adminFacade = adminFacade;
            this.storeManagementFacade = storeManagementFacade;
            this.notificationServer = notificationServer;
            this.logsServer = logsServer;
            if (logsServer.WebSocketServices[logsPath] == null)
                logsServer.AddWebSocketService<logsService>(logsPath);
        }

        [HttpPost("GetBuyerCart")]
        public ActionResult<Response<ServiceCart>> GetCart([FromBody] UserRequest request)
        {
            Response<ServiceCart> response = buyerFacade.GetCart(request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("AddProduct")]
        public ActionResult<Response<bool>> AddProductToCart([FromBody] AddProductToCartRequest request)
        {
            Response<bool> response = buyerFacade.AddProdcutToCart(request.UserId, request.StoreId, request.ProductId, request.Amount);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("RemoveProduct")]
        public ActionResult<Response<bool>> RemoveProductFromCart([FromBody] BuyingRequest request)
        {
            Response<bool> response = buyerFacade.RemoveProductFromCart(request.UserId, request.StoreId, request.ProductId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("ChangeProductAmount")]
        public ActionResult<Response<bool>> changeProductAmountInCart([FromBody] AddProductToCartRequest request)
        {
            Response<bool> response = buyerFacade.changeProductAmountInCart(request.UserId, request.StoreId, request.ProductId, request.Amount);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("PurchaseCart")]
        public ActionResult<Response<ServicePurchase>> PurchaseCartContent([FromBody] PurchaseRequest request)
        {
            ServicePaymentDetails paymentDetails = new ServicePaymentDetails(request.CardNumber, request.Month, request.Year, request.Holder, request.Ccv, request.Id);
            ServiceSupplyDetails supplyDetails = new ServiceSupplyDetails(request.ReceiverName, request.Address, request.City, request.Country, request.Zip);
            Response<ServicePurchase> response = buyerFacade.PurchaseCartContent(request.UserId, paymentDetails, supplyDetails);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("PurchaseBid")]
        public ActionResult<Response<bool>> PurchaseBid([FromBody] PurchaseBidRequest request)
        {
            ServicePaymentDetails paymentDetails = new ServicePaymentDetails(request.CardNumber, request.Month, request.Year, request.Holder, request.Ccv, request.Id);
            ServiceSupplyDetails supplyDetails = new ServiceSupplyDetails(request.ReceiverName, request.Address, request.City, request.Country, request.Zip);
            Response<bool> response = buyerFacade.PurchaseBid(request.StoreId, request.BidId, request.UserId, paymentDetails, supplyDetails);
            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("Enter")]
        public ActionResult<Response<int>> Enter()
        {
            Response<int> response = buyerFacade.Enter();

            if (response.IsErrorOccured())
                return BadRequest(response);

            logsServer.WebSocketServices[logsPath].Sessions.Broadcast("+Guest");

            return Ok(response);
        }

        [HttpPost("Leave")]
        public ActionResult<Response<bool>> Leave([FromBody] UserRequest request)
        {
            Response<bool> response = buyerFacade.Leave(request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            logsServer.WebSocketServices[logsPath].Sessions.Broadcast("-Guest");

            return Ok(response);
        }

        [HttpPost("StoreInfo")]
        public ActionResult<Response<ServiceStore>> GetStoreInfo([FromBody] StoreRequest request)
        {
            Response<ServiceStore> response;
            //if (request.StoreId > 0)
            response = buyerFacade.GetStoreInfo(request.StoreId);
            //else if (request.StoreName != null)
            //response = buyerFacade.GetStoreInfo(request.StoreName);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }


        [HttpPost("SerachProducts")]
        public ActionResult<Response<IDictionary<int, IList<ServiceProduct>>>> ProductsSearch([FromBody] SearchProductsRequest request)
        {
            Response<IDictionary<int, IList<ServiceProduct>>> response =
                buyerFacade.ProductsSearch(request.StoreName, request.ProductName, request.Category, request.Keyword, request.ProductId, request.ProductIds, request.memberInRole, request.storesWithProductsThatPassedFilter);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Register")]
        public ActionResult<Response<int>> Register([FromBody] AuthenticationRequest request)
        {
            Response<int> response = buyerFacade.Register(request.UserName, request.Password);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("GetUnsentMessages")]
        public ActionResult<Response<IList<string>>> GetUnsentMessage([FromBody] UsernameRequest request)
        {
            string username = request.Username;
            if (buyerUnsentMessages.ContainsKey(username))
            {
                IList<string> msgsCopy = buyerUnsentMessages[username].Select(e => e).ToList();
                var actionResult = Ok(new Response<IList<string>>(msgsCopy));
                buyerUnsentMessages[username].Clear();
                return actionResult;
            }
            return Ok(new Response<IList<string>>(new List<string>()));
        }

        private void AddUnsentMessage(string username, IList<string> messages)
        {
            buyerUnsentMessages[username] = messages;
        }

        [HttpPost("AdminIdentify")]
        public ActionResult<Response<bool>> AdminIdentify([FromBody] UserRequest request)
        {
            adminId = request.UserId;
            Response<bool> isAdmin = adminFacade.IsAdmin(adminId);
            if (isAdmin.IsErrorOccured())
            {
                return BadRequest(isAdmin);
            }
            return Ok(isAdmin);
        }

        [HttpPost("Login")]
        public ActionResult<Response<int>> Login([FromBody] AuthenticationRequestWithPort request)
        {
            string relativeServicePath = "/" + request.UserName + "-notifications";
            try
            {
                if (notificationServer.WebSocketServices[relativeServicePath] == null)
                    notificationServer.AddWebSocketService<NotificationsService>(relativeServicePath);
            }
            catch (ArgumentException ex)
            {
                return new Response<int>("Sorry, but it seems that we cant connect you");
            } // in case the client tries to login again

            Func<string[], bool> notifier = (msgs) =>
            {
                string username = request.UserName;

                // Try send, if not - add to unsent messages
                if (notificationServer.WebSocketServices[relativeServicePath] == null || notificationServer.WebSocketServices[relativeServicePath].Sessions.Count < 1)
                {
                    IList<string> unsentMsgs = new List<string>();
                    if (buyerUnsentMessages.ContainsKey(username))
                        unsentMsgs = buyerUnsentMessages[username];

                    // Adding new unsent messages
                    foreach (string msg in msgs)
                        unsentMsgs.Add(msg);

                    //this.buyerUnsentMessages[username] = unsentMsgs;
                    AddUnsentMessage(username, unsentMsgs);
                    return true; // So msgs delete on member msgs queue
                }

                foreach (string msg in msgs)
                    notificationServer.WebSocketServices[relativeServicePath].Sessions.Broadcast(msg);
                return true;
            };
            Response<int> response = buyerFacade.Login(request.UserName, request.Password, notifier);

            if (response.IsErrorOccured())
            {
                notificationServer.RemoveWebSocketService(relativeServicePath);
                return BadRequest(response);
            }

            buyerIdToRelativeNotificationPath.Add(response.Value, relativeServicePath);

            logsServer.WebSocketServices[logsPath].Sessions.Broadcast("-Guest");
            logsServer.WebSocketServices[logsPath].Sessions.Broadcast("+" + GetMembersRole(response.Value));

            return Ok(response);
        }

        private string GetMembersRole(int memberId)
        {
            Response<bool> isAdmin = adminFacade.IsAdmin(memberId);

            if (isAdmin.IsErrorOccured())
            {
                return "";
            }

            if (isAdmin.Value)
            {
                return "Admin";
            }

            Response<IDictionary<int, IList<ServiceProduct>>> owner =
            buyerFacade.ProductsSearch(null, null, null, null, null, null, new ServiceMemberInRole(memberId, Role.Owner), false);

            if (owner.IsErrorOccured())
            {
                return string.Empty;
            }

            ICollection<int> stores = owner.Value.Keys;
            if (stores.Contains(memberId))
            {
                return "Owner";
            }

            Response<IDictionary<int, IList<ServiceProduct>>> manager =
            buyerFacade.ProductsSearch(null, null, null, null, null, null, new ServiceMemberInRole(memberId, Role.Manager), false);

            if (manager.IsErrorOccured())
            {
                return string.Empty;
            }

            stores = manager.Value.Keys;
            if (stores.Contains(memberId))
            {
                return "Manager";
            }

            return "Memebr";
        }

        [HttpPost("Logout")]
        public ActionResult<Response<bool>> Logout([FromBody] UserRequest request)
        {
            Response<bool> response = buyerFacade.Logout(request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            // Clearing the connection
            if (buyerIdToRelativeNotificationPath.ContainsKey(request.UserId))
            {
                notificationServer.RemoveWebSocketService(buyerIdToRelativeNotificationPath[request.UserId]);
                buyerIdToRelativeNotificationPath.Remove(request.UserId);
            }

            logsServer.WebSocketServices[logsPath].Sessions.Broadcast("-" + GetMembersRole(request.UserId));

            return Ok(response);
        }

        [HttpPost("ReviewProduct")]
        public ActionResult<Response<bool>> AddProductReview([FromBody] ReviewProductRequest request)
        {
            Response<bool> response = buyerFacade.AddProductReview(request.UserId, request.StoreId, request.ProductId, request.Review);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

    }
}
