using UserDomain.Application.DTO;

namespace UserDomain.Presentation.UseCases
{
    public interface ITakeUserCredsListByIdsCase
    {
        public Task<Result<List<UserCredDTO>>> Handle(List<string> userIds);

    }
}
