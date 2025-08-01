using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Helper;
using UserRealetionShipDomain.Application.UseCases.UserFollowCases;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.FollowUserCases;

namespace UserRealetionShipDomain.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class FollowController(
        IUnFollowUserCase _unFollowUserCase,
        IGetUserFollowListCase _getUserFollowListCase,
        IGetFollowCountCase _getFollowCountCase,
        IFollowUserCase _followUserCase) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserFollowList()
        {
            string userId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId)) 
            {

                return BadRequest(new ProblemDetails
                {
                    Title = "Get user Follow List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }

            var result = await _getUserFollowListCase.Handle(userId);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else 
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Get user Follow List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = result.Error,
                    Status = 400,
                });
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<HttpMessage>> FollowUser([FromBody] string FollowId)
        {
            string userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {

                return BadRequest(new ProblemDetails
                {
                    Title = "Follow user Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }
            var result = await _followUserCase.Handle(userId, FollowId);

            if (result.IsSuccess)
            {
                return Created();
            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Follow user Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = result.Error,
                    Status = 400,
                });
            }
        }



        [HttpPost("[action]")]
        public async Task<ActionResult<HttpMessage>> UnFollowUser([FromBody] string FollowId)
        {
            string userId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
            {

                return BadRequest(new ProblemDetails
                {
                    Title = "UnFollow user Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }

            var result = await _unFollowUserCase.Handle(userId, FollowId);
            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "UnFollow user Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = result.Error,
                    Status = 400,
                });
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<HttpMessage<int>>> GetFollowCount()
        {
            string userId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
            {

                return BadRequest(new ProblemDetails
                {
                    Title = "Get follow count user Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }

            var result = await _getFollowCountCase.Handle(userId);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Get follow count user Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = result.Error,
                    Status = 400,
                });
            }

        }
    }
}
