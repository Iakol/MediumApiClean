using ApiGateway.Application.UseCases.UserRelationShipCases;
using ApiGateway.Application.UseCases.UserRelationShipCases.BlockCases;
using ApiGateway.Application.UseCases.UserRelationShipCases.FollowCases;
using ApiGateway.Application.UseCases.UserRelationShipCases.MuteCases;
using ApiGateway.Infrastructure.HttpService.UserRelationship;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiGateway.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class RelationshipController(
        //BlockUserCase _blockCase,
        //UnBlockUserCase _unblockCase,
        //MuteUserCase _MuteCase,
        //UnMuteUserCase _UnMuteCase,
        //FollowToUserCase _followCase,
        //UnFollowToUserCase _unfollowCase,
        FollowClient _FollowClient
        ) : ControllerBase
    {

        [HttpGet("[action]")]
        public async Task<IActionResult> TestAction() 
        {
            try
            {
                var responce = await _FollowClient.GetUserFollowList();
                return Ok(responce.Content.ReadFromJsonAsync<JsonElement>().Result);
            }
            catch (Exception ex) 
            {
                return Ok(ex.Message);
            }
            
        }

        //public async Task<IActionResult> BlockUser(string userId ) 
        //{
        //    return Ok(await _blockCase.BlockUserAsync(userId));
        //}
        //public async Task<IActionResult> UnBlockUser(string userId)
        //{
        //    return Ok(await _unblockCase.UnBlockUserAsync(userId));
        //}

        //public async Task<IActionResult> MuteUser(string userId)
        //{
        //    return Ok(await _MuteCase.MuteAsync(userId));
        //}
        //public async Task<IActionResult> UnMuteUser(string userId)
        //{
        //    return Ok(await _UnMuteCase.UnMuteAsync(userId));
        //}

        //public async Task<IActionResult> FollowUser(string userId)
        //{
        //    return Ok(await _followCase.FollowUserAsync(userId));
        //}
        //public async Task<IActionResult> UnFollowUser(string userId)
        //{
        //    return Ok(await _unfollowCase.UnFollowUserAsync(userId));
        //}
    }
}
