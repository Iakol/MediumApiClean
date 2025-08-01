using ApiGateway.Application.DTO;
using ApiGateway.Application.Interface;
using ApiGateway.Infrastructure.HttpService;
using System.Net;
using System.Reflection.Metadata;

namespace ApiGateway.Application.UseCases.AuthorUserCases
{
    public class GetAndAggregationUserCredDto(IUserService _UserService,IUserRealtionShipService _userRealtionShipService)
    {
        public async Task<UserCredDTO> HandleAsync(string userId) 
        {
            int followCount = await _userRealtionShipService.GetUserFollowersCountAsync(userId);
            UserCredUserDomainDTO user = await _UserService.GetUserCredAsync(userId);
            return new UserCredDTO
            {
                UserId = user.UserId,
                FollowersCounts = followCount,
                AboutUser = user.AboutUser,

            };
        }

        public  async Task<List<UserCredDTO>> HandleAsync(List<string> userId)
        {
            List<UserCredUserDomainDTO> user = await _UserService.GetUserCredAsync(userId);

            List <Task<UserCredDTO>> taskList = user.Select(async u =>
            {
                return new UserCredDTO
                {
                    UserId = u.UserId,
                    FollowersCounts = await _userRealtionShipService.GetUserFollowersCountAsync(u.UserId),
                    AboutUser = u.AboutUser
                };
            }).ToList();

            return (await Task.WhenAll(taskList)).ToList();            
        }
    }
}
