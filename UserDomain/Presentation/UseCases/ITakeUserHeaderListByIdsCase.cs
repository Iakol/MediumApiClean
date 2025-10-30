using UserDomain.Application.DTO;

namespace UserDomain.Presentation.UseCases
{
    public interface ITakeUserHeaderListByIdsCase
    {
        public Task<Result<List<UserHeaderDTO>>> Handle(List<string> userIds);

    }
}
