using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieTest.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthorizatuinController : ControllerBase
    {
        [HttpGet("[action]")]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> GetAuthorizedDataAdmins()
        {
            return Ok("value from admins");
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAuthorizedDataAdmin()
        {
            return Ok("value from admins");
        }

    }
}
