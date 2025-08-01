using ApiGateway.Application.DTO;
using ApiGateway.Application.Interface;

namespace ApiGateway.Application.UseCases.UserRelationShipCases.BlockCases
{
    public class GetBlockUserCase(IUserRealtionShipService _userRealtionShipService)
    {
        public async Task<List<string>> GetBlockUserAsync(string UserId)
        {
            return  await _userRealtionShipService.GetUserBlockedUserAsync(UserId);
        }
    }
}
