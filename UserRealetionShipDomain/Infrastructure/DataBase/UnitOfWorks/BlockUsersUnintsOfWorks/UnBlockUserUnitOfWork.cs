using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.UserBlockUnitsOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;

namespace UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.BlockUsersUnintsOfWorks
{
    public class UnBlockUserUnitOfWork(IUserBlockRepository _userBlockRepostitory, AppDbContext _db) : IUnBlockUserUnitOfWork
    {
        public async Task UnBlockuser(string UserId, string UserToBlock)
        {
            UserBlock BlockRecord = await _userBlockRepostitory.GetEntityAsync(UserId, UserToBlock);
            if (BlockRecord == null)
            {
                throw new Exception("The use was not Blocked");
            }
            else
            {
                await _userBlockRepostitory.DeleteAsync(UserId, UserToBlock);
                await _db.SaveChangesAsync();
            }
        }
    }
}
