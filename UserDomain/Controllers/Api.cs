using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserDomain.Application.DTO;
using UserDomain.Application.UserCases;

namespace UserDomain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Api(GetUserCredCase _getUserCredCase, GetUserHeaderCase _getUserHeaderCase) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<UserHeaderDTO>> GetUserHeader(string id) 
        {
            return await _getUserHeaderCase.GetUserHeader(id);
        }

        [HttpGet]
        public async Task<ActionResult<UserCredDTO>> GetUserCred(string id) 
        {
            return await _getUserCredCase.GetUsetCred(id);
        }
    }
}
