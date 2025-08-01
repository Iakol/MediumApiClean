using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.UserMuteCases;

namespace UserRealetionShipDomain.Application.UseCases.UserMuteCases
{
    public class GetUserMuteListCase(IUserMuteRepository _userMuteRepository,ILogger<GetUserMuteListCase> logger) : IGetUserMuteListCase
    {
        public async Task<Result<List<UserMute>>> Handle(string UserId) 
        {
            if (string.IsNullOrEmpty(UserId)) 
            {
                logger.LogError("The user ID is empty", "Error when Get Mute list of user");
                return Result<List<UserMute>>.Failure("The user ID is empty");
            }

            try
            {
                return Result<List<UserMute>>.Success(await _userMuteRepository.GetEntityListByFollowIdAsync(UserId));
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message, "Error when Get Mute list of user");
                return Result<List<UserMute>>.Failure(ex.Message);

            }
        }
    }
}
