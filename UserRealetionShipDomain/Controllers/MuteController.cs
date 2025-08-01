using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Helper;
using UserRealetionShipDomain.Application.UseCases.UserMuteCases;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.UserMuteCases;

namespace UserRealetionShipDomain.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MuteController
        (
        IGetUserMuteListCase _getUserMuteListCase,
        IMuteUserCase _muteUserCase,
        IUnMuteCase _unMuteCase
        ) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserMuteList()
        {
            string userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Get Users Mute List  Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }
            var result = await _getUserMuteListCase.Handle(userId);
            if (result.IsSuccess) 
            {
                return Ok(result.Data);
            }
            return BadRequest(new ProblemDetails
            {
                Title = "Get Users Mute List  Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> MuteUser([FromBody] string MuteId)
        {
            string userId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId)) 
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Mute user  Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }
            Result result = await _muteUserCase.Handle(userId, MuteId);
            if (result.IsSuccess)
            {
                
                return Created();
            }            
            return BadRequest(new ProblemDetails
            {
                Title = "Mute user  Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UnMuteUser( [FromBody] string MuteId)
        {
            string userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Get Users Mute List  Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }
            Result result = await _unMuteCase.Handle(userId, MuteId);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(new ProblemDetails
            {
                Title = "UnMute user  Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }
    }
}
