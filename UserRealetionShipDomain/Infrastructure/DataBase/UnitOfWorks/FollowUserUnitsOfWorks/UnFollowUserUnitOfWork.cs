using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.FollowUserUnitsOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;

namespace UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.FollowUserUnitsOfWorks
{
    public class UnFollowUserUnitOfWork(IUserFollowRepository _userFollowRepository,AppDbContext _db) : IUnFollowUserUnitOfWork
    {
        public async Task UnFollowuser(string UserId, string UserToFollow)
        {
            UserFollow userFollow = await _userFollowRepository.GetEntityAsync(UserId, UserToFollow);

            if (userFollow == null)
            {
                throw new Exception("User was not followed");
            }
            else 
            {
                await _userFollowRepository.DeleteAsync(UserId, UserToFollow);
                await _db.SaveChangesAsync();
            }
        }
    }
}
