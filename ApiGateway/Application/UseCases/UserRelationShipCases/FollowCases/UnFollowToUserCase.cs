using ApiGateway.Application.Interface;

namespace ApiGateway.Application.UseCases.UserRelationShipCases
{
    public class UnFollowToUserCase(IUserRealtionShipService _userRealtionShipService)
    {
        public async Task<bool> UnFollowUserAsync(string Id)
        {
            return (await _userRealtionShipService.FollowUserAsync(Id)).succses;
        }
    }
}
