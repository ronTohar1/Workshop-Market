using MarketBackend.ServiceLayer;
using MarketBackend.ServiceLayer.ServiceDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Requests;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminFacade adminFacade;

        public AdminController(AdminFacade adminFacade) => this.adminFacade = adminFacade;

        [HttpGet("BuyerPurchaseHistory/{request.TargetId}")]
        public ActionResult<Response<IReadOnlyCollection<ServicePurchase>>> GetBuyerPurchaseHistory
            ([FromBody] AdminRequest request)
        {
            Response<IReadOnlyCollection<ServicePurchase>> response = 
                adminFacade.GetBuyerPurchaseHistory(request.UserId, request.TargetId);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("StorePurchaseHistory/{request.TargetId}")]
        public ActionResult<Response<IReadOnlyCollection<ServicePurchase>>> GetStorePurchaseHistory
            ([FromBody] AdminRequest request)
        {
            Response<IReadOnlyCollection<ServicePurchase>> response =
                adminFacade.GetStorePurchaseHistory(request.UserId, request.TargetId);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("RemoveMember/{request.TargetId}")]
        public ActionResult<Response<bool>> RemoveMemberIfHasNoRoles
            ([FromBody] AdminRequest request)
        {
            Response<bool> response = adminFacade.RemoveMemberIfHasNoRoles(request.UserId, request.TargetId);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("LoggedInUsers")]
        public ActionResult<Response<bool>> GetLoggedInMembers
            ([FromBody] UserRequest request)
        {
            Response<IList<int>> response = adminFacade.GetLoggedInMembers(request.UserId);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("MemberInfo")]
        public ActionResult<Response<bool>> GetMemberInfo
            ([FromBody] AdminRequest request)
        {
            Response<ServiceMember> response = adminFacade.GetMemberInfo(request.UserId, request.TargetId);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

    }
}
