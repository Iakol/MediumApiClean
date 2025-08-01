using System.Collections.Generic;
using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.FollowUserCases;

namespace UserRealetionShipDomain.Application.UseCases.UserFollowCases
{
    public class GetUserFollowListCase(IUserFollowRepository _userFollowRepository, ILogger<GetUserFollowListCase> logger) : IGetUserFollowListCase
    {


        public async Task<Result<List<UserFollow>>> Handle(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogError("User Id is empty", "Error when Get User Follow Count");
                return Result<List<UserFollow>>.Failure("User Id is empty");
            }

            try
            {
                return Result < List < UserFollow >>.Success( await _userFollowRepository.GetEntityListByFollowIdAsync(userId));
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message, "Error when Get User Follow Count");
                return Result<List<UserFollow>>.Failure(ex.Message);
            }
        }
    }
}
