using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.UserMuteUnitsOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;

namespace UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.MuteUsersUnitsOfWorks
{
    public class MuteUserUnitOfWork(IUserMuteRepository _userMuteRepository,AppDbContext _db) : IMuteUserUnitOfWork
    {
        public async Task MuteUser(string UserId, string MuteId)
        {
            UserMute Usermute = await _userMuteRepository.GetEntityAsync(UserId, MuteId);
            if (Usermute != null)
            {
                throw new Exception("User alredy was mute");
            }
            else
            {
                UserMute newMute = new UserMute
                {
                    MuteAt = DateTime.Now,
                    UserId = UserId,
                    MuteId = MuteId

                };

                await _userMuteRepository.AddAsync(newMute);
                await _db.SaveChangesAsync();
            }
        }
    }
}
