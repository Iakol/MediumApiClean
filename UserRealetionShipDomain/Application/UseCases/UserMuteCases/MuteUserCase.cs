using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.UserMuteUnitsOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.MuteUsersUnitsOfWorks;
using UserRealetionShipDomain.Presentation.UseCases.UserMuteCases;

namespace UserRealetionShipDomain.Application.UseCases.UserMuteCases
{
    public class MuteUserCase(IMuteUserUnitOfWork _userMuteUnitOfwork, ILogger<MuteUserCase> logger) : IMuteUserCase
    {
        public async Task<Result> Handle(string UserId, string MuteId) 
        {
            if (string.IsNullOrEmpty(UserId)) 
            {
                logger.LogError("User Id is empty", "Error when MuteUser");
                return Result.Failure("User Id is empty");
            }

            if (string.IsNullOrEmpty(MuteId))
            {
                logger.LogError("User to mute Id is empty", "Error when MuteUser");
                return Result.Failure("User to mute id is empty");
            }

            try
            {
                await _userMuteUnitOfwork.MuteUser(UserId, MuteId);
                return Result.Success();
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message, "Error when MuteUser");
                return Result.Failure(ex.Message);
            }
        }

    }
}
