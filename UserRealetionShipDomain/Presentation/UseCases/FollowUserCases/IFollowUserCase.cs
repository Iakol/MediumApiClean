using UserRealetionShipDomain.Application.DTO;

namespace UserRealetionShipDomain.Presentation.UseCases.FollowUserCases
{
    public interface IFollowUserCase
    {
        public Task<Result> Handle(string UserId, string UserToFollow);

    }
}
