using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Helper;
using UserRealetionShipDomain.Application.UseCases.UserBlockCases;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.UserBlockCases;

namespace UserRealetionShipDomain.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BlockController(
        IGetUserBlockListCase _getUserBlockListCase,
        IBlockUserCase _blockUserCase,
        IUnBlockUserCase _unblockUserCase
        ) : ControllerBase
    {


        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserBlockList()
        {
            string userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Get user Block List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }
            Result<List<UserBlock>> result = await _getUserBlockListCase.Handle(userId);

            if (result.IsSuccess) 
            {
                return Ok(result.Data);
            }

            return BadRequest(new ProblemDetails
            {
                Title = "Get user Block List Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> BlockUser([FromBody] string blockId)
        {
            string userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Block user Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }

            Result result = await _blockUserCase.Handle(userId, blockId);
            if (result.IsSuccess)
            {
                return Created();
            }
            return BadRequest(new ProblemDetails
            {
                Title = "Block user Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UnBlockUser([FromBody] string blockId)
        {
            string userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "UnBlock user Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }
            Result result = await _unblockUserCase.Handle(userId, blockId);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(new ProblemDetails
            {
                Title = "UnBlock user Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
            
        }
    }
}
