using ApiGateway.Application.DTO;
using ApiGateway.Application.UseCases.AuthorUserCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
        (
        //GetAndAggregationUserHeaderDto _getUserHeaderCase,
        //GetAndAggregationUserCredDto _getUserCredCase
        ) : ControllerBase
    {
        //public async Task<ActionResult<UserHeaderDTO>> GetUserHeader(string userId) 
        //{
        //    return await _getUserHeaderCase.HandleAsync(userId);
        //}
        //public async Task<ActionResult<List<UserHeaderDTO>>> GetUserHeaderList(List<string> userIds)
        //{
        //    return await _getUserHeaderCase.HandleAsync(userIds);
        //}

        //public async Task<ActionResult<UserCredDTO>> GetUserCred(string userId)
        //{
        //    return await _getUserCredCase.HandleAsync(userId);
        //}
        //public async Task<ActionResult<List<UserCredDTO>>> GetUserCredList(List<string> userIds)
        //{
        //    return await _getUserCredCase.HandleAsync(userIds);
        //}
    }
}
