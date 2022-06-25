using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer;
using MarketBackend.ServiceLayer.Parsers;
using MarketBackend.ServiceLayer.ServiceDTO;
using MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO;
using MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAPI.Requests;
using ServicePurchase = MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServicePurchasePolicy;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreManagementFacade storeManagementFacade;

        public StoresController(IStoreManagementFacade storeManagementFacade) =>
            this.storeManagementFacade = storeManagementFacade;

        [HttpPost("AddNewProduct")]
        public ActionResult<Response<int>> AddNewProduct([FromBody] AddNewProductToStoreRequest request)
        {
            Response<int> response = storeManagementFacade.AddNewProduct(
                request.UserId, request.StoreId, request.ProductName, request.Price, request.Category);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("IncreaseProductAmount")]
        public ActionResult<Response<bool>> IncreaseProductAmountInInventory([FromBody] ChangeProductAmountInStoreRequest request)
        {
            Response<bool> response = storeManagementFacade.AddProductToInventory(
                request.UserId, request.StoreId, request.ProductId, request.Amount);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("DecreaseProductAmount")]
        public ActionResult<Response<bool>> DecreaseProductInInventory([FromBody] ChangeProductAmountInStoreRequest request)
        {
            Response<bool> response = storeManagementFacade.DecreaseProduct(
                request.UserId, request.StoreId, request.ProductId, request.Amount);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("MakeCoOwner")]
        public ActionResult<Response<bool>> MakeCoOwner([FromBody] RolesManagementRequest request)
        {
            Response<bool> response = storeManagementFacade.MakeCoOwner(
                request.UserId, request.TargetUserId, request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("RemoveCoOwner")]
        public ActionResult<Response<bool>> RemoveCoOwner([FromBody] RolesManagementRequest request)
        {
            Response<bool> response = storeManagementFacade.RemoveCoOwner(
                request.UserId, request.TargetUserId, request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("MakeCoManager")]
        public ActionResult<Response<bool>> MakeCoManager([FromBody] RolesManagementRequest request)
        {
            Response<bool> response = storeManagementFacade.MakeCoManager(
                request.UserId, request.TargetUserId, request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("MembersInRole")]
        public ActionResult<Response<IList<int>>> GetMembersInRole([FromBody] GetMembersInRoleRequest request)
        {
            Response<IList<int>> response = storeManagementFacade.GetMembersInRole(
                request.StoreId, request.UserId, request.Role);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Founder")]
        public ActionResult<Response<ServiceMember>> GetFounder([FromBody] StoreManagementRequest request)
        {
            Response<ServiceMember> response = storeManagementFacade.GetFounder(request.StoreId, request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("ManagerPermissions")]
        public ActionResult<Response<IList<Permission>>> GetManagerPermissions([FromBody] RolesManagementRequest request)
        {
            Response<IList<Permission>> response = storeManagementFacade.GetManagerPermissions(
                request.StoreId, request.UserId, request.TargetUserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("ChangeManagerPermission")]
        public ActionResult<Response<bool>> ChangeManagerPermission([FromBody] ChangePermissionsRequest request)
        {
            Response<bool> response = storeManagementFacade.ChangeManagerPermission(
                request.UserId, request.TargetUserId, request.StoreId, request.Permissions);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("PurchaseHistory")]
        public ActionResult<Response<IList<Purchase>>> GetPurchaseHistory([FromBody] StoreManagementRequest request)
        {
            Response<IList<Purchase>> response = storeManagementFacade.GetPurchaseHistory(
                request.UserId, request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("OpenNewStore")]
        public ActionResult<Response<int>> OpenNewStore([FromBody] OpenStoreRequest request)
        {
            Response<int> response = storeManagementFacade.OpenNewStore(
                request.UserId, request.StoreName);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("CloseStore")]
        public ActionResult<Response<bool>> CloseStore([FromBody] StoreManagementRequest request)
        {
            Response<bool> response = storeManagementFacade.CloseStore(
                request.UserId, request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("AddDiscountPolicy")]
        public ActionResult<Response<int>> AddDiscountPolicy([FromBody] AddDiscountPolicyRequest request)
        {
            ServiceExpression exp = DiscountAndPolicyParser.ConvertDiscountFromJson(request.Expression);
            Response<int> response = storeManagementFacade.AddDiscountPolicy(
                exp, request.Description, request.StoreId, request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("RemoveDiscountPolicy")]
        public ActionResult<Response<bool>> RemoveDiscountPolicy([FromBody] RemovePolicyRequest request)
        {
            Response<bool> response = storeManagementFacade.RemoveDiscountPolicy(
                request.PolicyId, request.StoreId, request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("AddPurchasePolicy")]
        public ActionResult<Response<int>> AddPurchasePolicy([FromBody] AddPurchasePolicyRequest request)
        {
            MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServicePurchasePolicy policy = DiscountAndPolicyParser.ConvertPolicyFromJson(request.Expression);
            Response<int> response = storeManagementFacade.AddPurchasePolicy(
                policy, request.Description, request.StoreId, request.UserId);
            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("RemovePurchasePolicy")]
        public ActionResult<Response<bool>> RemovePurchasePolicy([FromBody] RemovePolicyRequest request)
        {
            Response<bool> response = storeManagementFacade.RemovePurchasePolicy(
                request.PolicyId, request.StoreId, request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("AddBid")]
        public ActionResult<Response<int>> AddBid([FromBody] AddBidRequest request)
        {
            Response<int> response = storeManagementFacade.AddBid(
                request.StoreId, request.ProductId, request.MemberId, request.BidPrice);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("ApproveBid")]
        public ActionResult<Response<bool>> ApproveBid([FromBody] ApproveBidRequest request)
        {
            Response<bool> response = storeManagementFacade.ApproveBid(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("DenyBid")]
        public ActionResult<Response<bool>> DenyBid([FromBody] DenyBidRequest request)
        {
            Response<bool> response = storeManagementFacade.DenyBid(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("MakeCounterOffer")]
        public ActionResult<Response<bool>> MakeCounterOffer([FromBody] MakeCounterOfferRequest request)
        {
            Response<bool> response = storeManagementFacade.MakeCounterOffer(
                request.StoreId, request.MemberId, request.BidId, request.Offer);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("GetApproveForBid")]
        public ActionResult<Response<IList<int>>> GetApproveForBid([FromBody] GetApproveForBidRequest request)
        {
            Response<IList<int>> response = storeManagementFacade.GetApproveForBid(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("RemoveBid")]
        public ActionResult<Response<bool>> RemoveBid([FromBody] RemoveBidRequest request)
        {
            Response<bool> response = storeManagementFacade.RemoveBid(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("ApproveCounterOffer")]
        public ActionResult<Response<bool>> ApproveCounterOffer([FromBody] ApproveCounterOfferRequest request)
        {
            Response<bool> response = storeManagementFacade.ApproveCounterOffer(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPut("DenyCounterOffer")]
        public ActionResult<Response<bool>> DenyCounterOffer([FromBody] DenyCounterOfferRequest request)
        {
            Response<bool> response = storeManagementFacade.DenyCounterOffer(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("GetProductReviews")]
        public ActionResult<Response<IDictionary<string, IList<string>>>> GetProductReviews([FromBody] GetProductReviewsRequest request)
        {
            Response<IDictionary<string, IList<string>>> response = storeManagementFacade.GetProductReviews(
                request.StoreId, request.ProductId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("AddProductReview")]
        public ActionResult<Response<IDictionary<ServiceMember, IList<string>>>> AddProductReview([FromBody] AddProductReviewRequest request)
        {
            Response<bool> response = storeManagementFacade.AddProductReview(
                request.StoreId, request.MemberId, request.ProductId, request.Review);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("GetDailyProfit")]
        public ActionResult<Response<double>> GetDailyProfit([FromBody] GetStoreDailyProfitRequest request)
        {
            Response<double> response = storeManagementFacade.GetStoreDailyProfit(
                request.StoreId, request.MemberId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("MakeNewCoOwner")]
        public ActionResult<Response<bool>> MakeNewCoOwner([FromBody] RolesManagementRequest request)
        {
            Response<bool> response = storeManagementFacade.MakeNewCoOwner(
                request.UserId, request.TargetUserId, request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("DenyNewCoOwner")]
        public ActionResult<Response<bool>> DenyNewCoOwner([FromBody] RolesManagementRequest request)
        {
            Response<bool> response = storeManagementFacade.DenyNewCoOwner(
                request.UserId, request.TargetUserId, request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }


    }
}
