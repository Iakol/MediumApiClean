using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.UserBlockUnitsOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;

namespace UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.BlockUsersUnintsOfWorks
{
    public class BlockUserUnitOfWork(IUserBlockRepository _userBlockRepostitory, AppDbContext _db) : IBlockUserUnitOfWork
    {
        public async Task Blockuser(string UserId, string UserToBlock) 
        {
            UserBlock BlockRecord = await _userBlockRepostitory.GetEntityAsync(UserId, UserToBlock);


            if (BlockRecord != null)
            {
                throw new Exception("User Already Blocked");
            }
            else
            {
                UserBlock newBlock = new UserBlock
                {
                    BlockAt = DateTime.Now,
                    UserId = UserId,
                    BlockId = UserToBlock

                };

                await _userBlockRepostitory.AddAsync(newBlock);
                await _db.SaveChangesAsync();
            }
        }

    }
}
