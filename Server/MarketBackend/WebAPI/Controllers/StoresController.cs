using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer;
using MarketBackend.ServiceLayer.ServiceDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Requests;

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

        [HttpPut("AddProduct")]
        public ActionResult<Response<bool>> AddProductToInventory([FromBody] ChangeProductAmountInStoreRequest request)
        {
            Response<bool> response = storeManagementFacade.AddProductToInventory(
                request.UserId, request.StoreId, request.ProductId, request.Amount);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("DecreaseProduct")]
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
                request.UserId ,request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("OpenNewStore")]
        public ActionResult<Response<int>> OpenNewStore([FromBody] OpenStoreRequest request)
        {
            Response<int> response = storeManagementFacade.OpenStore(
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
            Response<int> response = storeManagementFacade.AddDiscountPolicy(
                request.Expression, request.Description, request.StoreId, request.UserId);

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
            Response<int> response = storeManagementFacade.AddPurchasePolicy(
                request.Expression, request.Description, request.StoreId, request.UserId);

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

    }
}
