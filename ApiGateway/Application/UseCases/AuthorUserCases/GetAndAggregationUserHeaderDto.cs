using ApiGateway.Application.DTO;
using ApiGateway.Application.Interface;

namespace ApiGateway.Application.UseCases.AuthorUserCases
{
    public class GetAndAggregationUserHeaderDto(IUserService _UserService)
    {
        public async Task<UserHeaderDTO> HandleAsync(string userId)
        {
            return await _UserService.GetHeaderCredAsync(userId);
            
        }

        public async Task<List<UserHeaderDTO>> HandleAsync(List<string> userId)
        {
            return  await _UserService.GetHeaderCredAsync(userId);

            

        }
    }
}
