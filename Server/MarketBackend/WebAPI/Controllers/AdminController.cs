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
        private readonly IAdminFacade adminFacade;

        public AdminController(IAdminFacade adminFacade) => this.adminFacade = adminFacade;

        [HttpPost("BuyerPurchaseHistory/{request.TargetId}")]
        public ActionResult<Response<IReadOnlyCollection<ServicePurchase>>> GetBuyerPurchaseHistory
            ([FromBody] AdminRequest request)
        {
            Response<IReadOnlyCollection<ServicePurchase>> response = 
                adminFacade.GetBuyerPurchaseHistory(request.UserId, request.TargetId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("StorePurchaseHistory/{request.TargetId}")]
        public ActionResult<Response<IReadOnlyCollection<ServicePurchase>>> GetStorePurchaseHistory
            ([FromBody] AdminRequest request)
        {
            Response<IReadOnlyCollection<ServicePurchase>> response =
                adminFacade.GetStorePurchaseHistory(request.UserId, request.TargetId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("RemoveMember/{request.TargetId}")]
        public ActionResult<Response<bool>> RemoveMemberIfHasNoRoles([FromBody] AdminRequest request)
        {
            Response<bool> response = adminFacade.RemoveMemberIfHasNoRoles(request.UserId, request.TargetId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("LoggedInUsers")]
        public ActionResult<Response<IList<ServiceMember>>> GetLoggedInMembers([FromBody] UserRequest request)
        {
            Response<IList<ServiceMember>> response = adminFacade.GetLoggedInMembers(request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("MemberInfo")]
        public ActionResult<Response<ServiceMember>> GetMemberInfo([FromBody] AdminRequest request)
        {
            Response<ServiceMember> response = adminFacade.GetMemberInfo(request.UserId, request.TargetId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("IsAdmin")]
        public ActionResult<Response<bool>> IsAdmin([FromBody] UserRequest request)
        {
            Response<bool> response = adminFacade.IsAdmin(request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("GetSystemDailyProfit")]
        public ActionResult<Response<double>> GetSystemDailyProfit([FromBody] GetStoreDailyProfitRequestAdmin request)
        {
            Response<double> response = adminFacade.GetSystemDailyProfit(request.MemberId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("GetEventLogs")]
        public ActionResult<Response<string>> GetEventLogs([FromBody] UserRequest request)
        {
            Response<string> response = adminFacade.GetEventLogs(request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("GetErrorLogs")]
        public ActionResult<Response<string>> GetErrorLogs([FromBody] UserRequest request)
        {
            Response<string> response = adminFacade.GetErrorLogs(request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
		[HttpPost("GetDailyVisitorsCut")]
        public ActionResult<Response<int[]>> GetDailyVisitores([FromBody] GetDailyVisitoresRequestAdmin request)
        {
            Response<int[]> response = adminFacade.GetDailyVisitores(request.MemberId,new DateTime(request.FromYear, request.FromMonth, request.FromDay), new DateTime(request.ToYear, request.ToMonth, request.ToDay));

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
    }
}