using AutoMapper;
using UserDomain.Application.DTO;
using UserDomain.Application.Interfaces.Repository;
using UserDomain.Infrastructure.Database.DBContext;
using UserDomain.Infrastructure.Database.Repositories.UserRepositories;

namespace UserDomain.Application.UserCases
{
    public class GetUserCredCase( IUserProfileRepository _userProfileRepository , IMapper _mapper)
    {
        public async Task<UserCredDTO> GetUsetCred(string Id) 
        {

            var UserProfile = await _userProfileRepository.GetEntityAsync(Id);

            return _mapper.Map<UserCredDTO>(UserProfile);


        }
    }
}
