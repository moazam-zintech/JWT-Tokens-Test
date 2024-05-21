using JWT_Tokens_Test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Tokens_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        /*    [HttpGet]
            public string GetData()
            {
                return "Auth with JWT";
            }
        */
        [Authorize]
        [HttpGet]
        public string GetDetail()
        {
            return "Auth with JWT";
        }
        [HttpPost]
        public string AddUser(Users users)
        {
            return
                "User Added" + users.userName;
        }
    }
}
