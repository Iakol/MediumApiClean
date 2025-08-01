using ApiGateway.Application.DTO;
using ApiGateway.Application.Interface;

namespace ApiGateway.Application.UseCases.UserRelationShipCases
{
    public class UnBlockUserCase(IUserRealtionShipService _userRealtionShipService)
    {


        public async Task<bool> UnBlockUserAsync(string UserId)
        {
            httpMessage result = await _userRealtionShipService.UnBlockUserAsync(UserId);
            return result.succses;
        }
    }
}
