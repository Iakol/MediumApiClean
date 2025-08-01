using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.FollowUserUnitsOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.FollowUserUnitsOfWorks;
using UserRealetionShipDomain.Presentation.UseCases.FollowUserCases;

namespace UserRealetionShipDomain.Application.UseCases.UserFollowCases
{
    public class FollowUserCase(IFollowUserUnitOfWork _userFollowUinit, ILogger<FollowUserCase> logger) : IFollowUserCase
    {
        public async Task<Result> Handle(string UserId, string UserToFollow)
        {
            if (string.IsNullOrWhiteSpace(UserId)) 
            {
                logger.LogError("User id is empty", "Error when Follow User");
                return Result.Failure("User id is empty");
            }
            if (string.IsNullOrWhiteSpace(UserToFollow))
            {
                logger.LogError("User to follow id is empty", "Error when Follow User");
                return Result.Failure("User to follow id is empty");
            }
            try
            {
                await _userFollowUinit.Followuser(UserId, UserToFollow);
                return Result.Success();
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message, "Error when Follow User");
                return Result.Failure(ex.Message);

            }
        }
    }
}
