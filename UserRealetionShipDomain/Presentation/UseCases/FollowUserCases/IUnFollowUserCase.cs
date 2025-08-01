using UserRealetionShipDomain.Application.DTO;

namespace UserRealetionShipDomain.Presentation.UseCases.FollowUserCases
{
    public interface IUnFollowUserCase
    {
        public Task<Result> Handle(string UserId, string UserToFollow);

    }
}
