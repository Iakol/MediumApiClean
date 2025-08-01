using AutoMapper;
using UserDomain.Application.DTO;
using UserDomain.Application.Interfaces.Repository;
using UserDomain.Domain;

namespace UserDomain.Application.UserCases
{
    public class GetUserHeaderCase(IUserRepository _userRepository, IUserProfileRepository _userProfileRepository, IUserWrapperRepository _userWrapperRepository)
    {
        public async Task<UserHeaderDTO> GetUserHeader(string id) 
        {
            Task<UserProfile> profileTask =  _userProfileRepository.GetEntityAsync(id);
            Task<User> UserTask = _userRepository.GetEntityAsync(id);
            Task<UserWrapper> UserWrapperTask = _userWrapperRepository.GetEntityAsync(id);


            await Task.WhenAll(profileTask, UserTask, UserWrapperTask);

            UserProfile profile = await profileTask;
            User user = await UserTask;
            UserWrapper wrapper = await UserWrapperTask;

            return new UserHeaderDTO
            {
                UserId = id,
                CreatorName = user.UserName,
                CreatorLogo =  profile.LogoUrl,
                CreatorTag = wrapper.Tag

            };

        }
    }
}
