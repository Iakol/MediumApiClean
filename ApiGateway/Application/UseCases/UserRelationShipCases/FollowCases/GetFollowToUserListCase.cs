using ApiGateway.Application.Interface;

namespace ApiGateway.Application.UseCases.UserRelationShipCases.FollowCases
{
    public class GetFollowToUserListCase(IUserRealtionShipService _userRealtionShipService)
    {
        public async Task<List<string>> GetFollowToUserList(string UserId)
        {
            return await _userRealtionShipService.GetUserFollowersAsync(UserId);
        }
    }
}
