using System.Collections.Generic;
using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.UserBlockCases;

namespace UserRealetionShipDomain.Application.UseCases.UserBlockCases
{
    public class GetUserBlockListCase(IUserBlockRepository _userBlockRepostitory, ILogger<GetUserBlockListCase> logger) : IGetUserBlockListCase
    {
        public async Task<Result<List<UserBlock>>> Handle(string UserId) 
        {
            if (string.IsNullOrWhiteSpace(UserId)) 
            {
                logger.LogError("User Id is empty", "Error when Get List of Block Users");
                return Result<List<UserBlock>>.Failure("User Id is empty");
            }
            try
            {
                return Result<List<UserBlock>>.Success(await _userBlockRepostitory.GetEntityListByFollowIdAsync(UserId));

            } catch (Exception ex) 
            {
                logger.LogError(ex.Message, "Error when Get List of Block Users");
                return Result<List<UserBlock>>.Failure(ex.Message);

            }
        }
    }
}
