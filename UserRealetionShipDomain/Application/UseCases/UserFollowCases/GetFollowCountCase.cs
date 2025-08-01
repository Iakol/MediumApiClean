using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.FollowUserCases;

namespace UserRealetionShipDomain.Application.UseCases.UserFollowCases
{
    public class GetFollowCountCase(IUserFollowRepository _userFollowRepository,ILogger<GetFollowCountCase> logger) : IGetFollowCountCase
    {
        public async Task<Result<int>> Handle(string userId)
        {

            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogError("User Id is empty", "Error when Get User Follow Count");
                return Result<int>.Failure("User Id is empty");
            }

            try
            {

                return Result<int>.Success((await _userFollowRepository.GetEntityListByFollowIdAsync(userId)).Count());
            } catch (Exception ex) 
            {
                logger.LogError(ex.Message, "Error when Get User Follow Count");
                return Result<int>.Failure(ex.Message);
            }
        }
    }
}
