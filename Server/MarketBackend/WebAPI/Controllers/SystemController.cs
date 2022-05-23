using MarketBackend.ServiceLayer;
using MarketBackend.ServiceLayer.ServiceDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Requests;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystemOperator systemOperator;

        public SystemController(ISystemOperator systemOperator) => this.systemOperator = systemOperator;

        [HttpPost("OpenMarket")]
        public ActionResult<Response<int>> OpenMarket([FromBody] OpenMarketRequest request)
        {
            Response<int> response = systemOperator.OpenMarket(request.UserName, request.Password);

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("CloseMarket")]
        public ActionResult<Response<bool>> CloseMarket()
        {
            Response<bool> response = systemOperator.CloseMarket();

            if (response.ErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

    }
}
