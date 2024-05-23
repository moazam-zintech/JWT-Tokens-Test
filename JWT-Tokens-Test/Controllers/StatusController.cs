using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

namespace JWT_Tokens_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
      [Authorize] //This Tage is used to add Authentication (End ppoint)
        [HttpGet]
        public ActionResult AuthStatus()
        {
            var response = new
            {
                Message = "Auth is up:",
                ServerTime = DateTime.Now,
            };
            return Ok(response);
        }


    }
}
