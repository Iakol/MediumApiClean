using UserDomain.Application.DTO;
using UserDomain.Presentation.UseCases;

namespace UserDomain.Application.UserCases
{
    public class TakeUserCredsListByIdsCase : ITakeUserCredsListByIdsCase
    {
        public async Task<Result<List<UserCredDTO>>> Handle(List<string> userIds)
        {

            if (userIds.Count <= 0)
            {
                return Result<List<UserCredDTO>>.Failure("User ids list is null");
            }


            

        }
    }
}
