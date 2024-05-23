using JWT_Tokens_Test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace JWT_Tokens_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        private Users AuthenticateUser(Users users)
        {
            Users _user = null;

                      if (users.userName == "admin" && users.password == "1234")
            {
                            _user = new Users
                {
                            userName = "Hello"
                };
            }
            return _user;
        }
        private string GenrateTokens(Users user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var tokens = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokens);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users users)
        {
            IActionResult response = Unauthorized();
            var _user = AuthenticateUser(users);
            if (_user != null)
            {
                var token = GenrateTokens(users);
                response = Ok(new { token = token });
            }
            return response;
        }
    }
}
