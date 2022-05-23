using MarketBackend.ServiceLayer;
using MarketBackend.ServiceLayer.ServiceDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Requests;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : Controller
    {
        private readonly IBuyerFacade buyerFacade;

        public BuyersController(IBuyerFacade buyerFacade) => this.buyerFacade = buyerFacade;

        [HttpPost]
        public ActionResult<Response<ServiceCart>> GetCart([FromBody] UserRequest request)
        {
            Response<ServiceCart> response = buyerFacade.GetCart(request.UserId);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("AddProduct")]
        public ActionResult<Response<bool>> AddProductToCart([FromBody] AddProductToCartRequest request)
        {
            Response<bool> response = buyerFacade.AddProdcutToCart(request.UserId, request.StoreId, request.ProductId, request.Amount);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("RemoveProduct")]
        public ActionResult<Response<bool>> RemoveProductFromCart([FromBody] BuyingRequest request)
        {
            Response<bool> response = buyerFacade.RemoveProductFromCart(request.UserId, request.StoreId, request.ProductId);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("ChangeProductAmount")]
        public ActionResult<Response<bool>> changeProductAmountInCart([FromBody] AddProductToCartRequest request)
        {
            Response<bool> response = buyerFacade.changeProductAmountInCart(request.UserId, request.StoreId, request.ProductId, request.Amount);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("PurchaseCart")]
        public ActionResult<Response<bool>> PurchaseCartContent([FromBody] UserRequest request)
        {
            Response<ServicePurchase> response = buyerFacade.PurchaseCartContent(request.UserId);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("Enter")]
        public ActionResult<Response<bool>> Enter()
        {
            Response<int> response = buyerFacade.Enter();

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Leave")]
        public ActionResult<Response<bool>> Leave([FromBody] UserRequest request)
        {
            Response<bool> response = buyerFacade.Leave(request.UserId);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("StoreInfo")]
        public ActionResult<Response<bool>> GetStoreInfo([FromBody] StoreRequest request)
        {
            Response<ServiceStore> response;
            if (request.StoreId > 0)
                response = buyerFacade.GetStoreInfo(request.StoreId);
            else
                response = buyerFacade.GetStoreInfo(request.StoreName);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("SerachProducts")]
        public ActionResult<Response<bool>> ProductsSearch([FromBody] SearchProductsRequest request)
        {
            Response<IDictionary<int, IList<ServiceProduct>>> response = 
                buyerFacade.ProductsSearch(request.StoreName, request.ProductName, request.Category, request.Keyword);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Register")]
        public ActionResult<Response<bool>> Register([FromBody] AuthenticationRequest request)
        {
            Response<int> response = buyerFacade.Register(request.UserName, request.Password);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Login")]
        public ActionResult<Response<bool>> Login([FromBody] AuthenticationRequest request)
        {
            Response<int> response = buyerFacade.Login(request.UserName, request.Password,
                (msgs) => { return false; });

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Logout")]
        public ActionResult<Response<bool>> Logout([FromBody] UserRequest request)
        {
            Response<bool> response = buyerFacade.Logout(request.UserId);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("ReviewProduct")]
        public ActionResult<Response<bool>> AddProductReview([FromBody] ReviewProductRequest request)
        {
            Response<bool> response = buyerFacade.AddProductReview(request.UserId,request.StoreId, request.ProductId, request.Review);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

    }
}
