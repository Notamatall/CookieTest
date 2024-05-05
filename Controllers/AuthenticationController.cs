using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookieTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        public class User
        {
            public string? Username { get; set; }
        }

        [HttpPost("loginCookie")]
        public async Task<IActionResult> LoginCookie([FromBody] User user)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username ?? string.Empty));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                });

            return Ok();
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Forbid();
        }

        [HttpGet("forbidden")]
        public IActionResult GetForbidden()
        {
            return Forbid();
        }

    }
}
