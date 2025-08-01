using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.UserMuteUnitsOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;

namespace UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.MuteUsersUnitsOfWorks
{
    public class UnMuteUserUnitOfWork(IUserMuteRepository _userMuteRepository, AppDbContext _db) : IUnMuteUserUnitOfWork
    {
        public async Task UnMuteUser(string UserId, string MuteId) 
        {
            UserMute userMute = await _userMuteRepository.GetEntityAsync(UserId, MuteId);
            if (userMute == null)
            {
                throw new Exception("The User did`t mute");
            }
            else
            {
                await _userMuteRepository.DeleteAsync(UserId, MuteId);
                await _db.SaveChangesAsync();
            }
        }
    }
}
