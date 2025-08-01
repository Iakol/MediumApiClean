using UserRealetionShipDomain.Application.DTO;

namespace UserRealetionShipDomain.Presentation.UseCases.FollowUserCases
{
    public interface IGetFollowCountCase
    {
        public Task<Result<int>> Handle(string userId);
    }
}
