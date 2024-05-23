using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

namespace JWT_Tokens_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
      [Authorize(Roles ="Admin")] //This Tage is used to add Authentication and Authorization Now i give the Role that only admin have authority
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
