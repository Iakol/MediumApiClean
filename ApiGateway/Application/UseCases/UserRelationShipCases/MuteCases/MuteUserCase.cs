using ApiGateway.Application.DTO;
using ApiGateway.Application.Interface;

namespace ApiGateway.Application.UseCases.UserRelationShipCases
{
    public class MuteUserCase(IUserRealtionShipService _userRealtionShipService)
    {
        public async Task<bool> MuteAsync(string UserId) 
        {
            httpMessage result = await _userRealtionShipService.MuteUserAsync(UserId);
            return result.succses;
        }
    }
}
