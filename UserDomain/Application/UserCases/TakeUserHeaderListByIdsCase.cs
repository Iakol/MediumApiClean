using UserDomain.Application.DTO;
using UserDomain.Presentation.UseCases;

namespace UserDomain.Application.UserCases
{
    public class TakeUserHeaderListByIdsCase : ITakeUserHeaderListByIdsCase
    {
        public Task<Result<List<UserHeaderDTO>>> Handle(List<string> userIds)
        {
            throw new NotImplementedException();
        }
    }
}
