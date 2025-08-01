using ApiGateway.Application.DTO;
using ApiGateway.Application.Interface;

namespace ApiGateway.Application.UseCases.UserRelationShipCases.MuteCases
{
    public class GetMuteUserCase(IUserRealtionShipService _userRealtionShipService)
    {
        public async Task<List<string>> GetMuteUser(string UserId)
        {
            return await _userRealtionShipService.GetUserMutedUserAsync(UserId);
        }
    }
}
