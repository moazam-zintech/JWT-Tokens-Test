using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace JWT_Tokens_Test.Auth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        //private readonly IOptions<AuthenticationSchemeOptions> _options;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory loggerFactory,
            UrlEncoder urlEncoder,
            ISystemClock systemClock): base(options,loggerFactory,urlEncoder,systemClock) 
        {

        }
        
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("UnAuthorizad");
            }
            string autherizationHeader = Request.Headers["Authorization"];
            if(!autherizationHeader.StartsWith("basic ",StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail($"{autherizationHeader}");
            }
            var token = autherizationHeader.Substring(6);
            var credentialsAsString=Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var cradentials=credentialsAsString.Split(":");

            var username = cradentials[0];
            var password = cradentials[1];  
            if(username != "like" && password != "subscribe")
            {
                return AuthenticateResult.Fail("");
            }
            //We create Array of Claim 
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Role,"Admin")}; 
            var identity=new ClaimsIdentity(claims,"basic");
            var claimPriciple=new ClaimsPrincipal(identity);
            return AuthenticateResult.Success(new AuthenticationTicket(claimPriciple, Scheme.Name));
        }
    }
}
