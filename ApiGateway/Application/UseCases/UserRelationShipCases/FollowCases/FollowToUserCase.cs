using ApiGateway.Application.DTO;
using ApiGateway.Application.Interface;

namespace ApiGateway.Application.UseCases.UserRelationShipCases.FollowCases
{
    public class FollowToUserCase(IUserRealtionShipService _userRealtionShipService)
    {
        public async Task<bool> FollowUserAsync(string Id) 
        {
            return (await _userRealtionShipService.FollowUserAsync(Id)).succses;
        }
    }
}
