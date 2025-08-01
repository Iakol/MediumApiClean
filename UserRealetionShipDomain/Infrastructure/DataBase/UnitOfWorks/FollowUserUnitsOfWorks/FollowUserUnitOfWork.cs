using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.FollowUserUnitsOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;

namespace UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.FollowUserUnitsOfWorks
{
    public class FollowUserUnitOfWork(IUserFollowRepository _userFollowRepository, AppDbContext _db) : IFollowUserUnitOfWork
    {
        public async Task Followuser(string UserId, string UserToFollow)
        {
            UserFollow userFollow = await _userFollowRepository.GetEntityAsync(UserId, UserToFollow);

            if (userFollow != null)
            {
                throw new Exception("User was follow");
            }
            else
            {
                UserFollow userTofollow = new UserFollow
                {
                    UserId = UserId,
                    FollowId = UserToFollow,
                    FollowAt = DateTime.Now,
                };
                await _userFollowRepository.AddAsync(userTofollow);
                await _db.SaveChangesAsync();
            }
        }
    }
}
