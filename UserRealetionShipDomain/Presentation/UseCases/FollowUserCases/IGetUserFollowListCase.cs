using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Domain;

namespace UserRealetionShipDomain.Presentation.UseCases.FollowUserCases
{
    public interface IGetUserFollowListCase
    {
        public Task<Result<List<UserFollow>>> Handle(string userId);
    }
}
