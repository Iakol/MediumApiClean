using ApiGateway.Application.DTO;
using ApiGateway.Application.Interface;

namespace ApiGateway.Application.UseCases.UserRelationShipCases.BlockCases
{
    public class BlockUserCase(IUserRealtionShipService _userRealtionShipService)
    {
        public async Task<bool> BlockUserAsync(string UserId)
        {
            httpMessage result = await _userRealtionShipService.BlockUserAsync(UserId);
            return result.succses;

        }

    }
}
