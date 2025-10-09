using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UserDomain.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        [HttpPost("[action]")]
        public async Task<IActionResult> FindByEmailAsync([FromBody]string Email) 
        {
            return Ok(await _userManager.FindByEmailAsync(Email));
        }
    }
}
