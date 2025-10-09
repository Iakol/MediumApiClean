using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndentityDomain.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        public OAuthController() { }


        [HttpPost("[action]")]
        public async Task<IActionResult> Google([FromBody] string idToken) 
        {

            return Ok();
        }
    }
}
