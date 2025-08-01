using ApiGateway.Application.DTO;
using ApiGateway.Application.Interface;

namespace ApiGateway.Application.UseCases.UserRelationShipCases.MuteCases
{
    public class UnMuteUserCase(IUserRealtionShipService _userRealtionShipService)
    {

        public async Task<bool> UnMuteAsync(string UserId)
        {
            httpMessage result = await _userRealtionShipService.UnMuteUserAsync(UserId);
            return result.succses;
        }
    }
}

