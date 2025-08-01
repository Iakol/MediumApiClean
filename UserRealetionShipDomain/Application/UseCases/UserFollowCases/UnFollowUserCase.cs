using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.FollowUserUnitsOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.FollowUserCases;

namespace UserRealetionShipDomain.Application.UseCases.UserFollowCases
{
    public class UnFollowUserCase(IUnFollowUserUnitOfWork _userFollowUnit, ILogger<UnFollowUserCase> logger) : IUnFollowUserCase
    {
        public async Task<Result> Handle(string UserId, string UserToFollow)
        {
            if (string.IsNullOrWhiteSpace(UserId)) 
            {
                logger.LogError("User Id is empty", "Error when Unfollow User ");
                return Result.Failure("User Id is empty");
            }
            if (string.IsNullOrWhiteSpace(UserToFollow))
            {
                logger.LogError("User to follow Id is empty", "Error when Unfollow User ");
                return Result.Failure("User to follow Id is empty");
            }

            try
            {
                await _userFollowUnit.UnFollowuser(UserId, UserToFollow);
                return Result.Success();
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message, "Error when Unfollow User ");
                return Result.Failure(ex.Message);

            }
        }
    }
}
