using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.UserBlockUnitsOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.UserBlockCases;

namespace UserRealetionShipDomain.Application.UseCases.UserBlockCases
{
    public class BlockUserCase(IBlockUserUnitOfWork _blockUserUnitOfWork, ILogger<BlockUserCase> logger) : IBlockUserCase
    {
        public async Task<Result> Handle(string UserId, string UserToBlock)
        {
            if (string.IsNullOrEmpty(UserId)) 
            {
                logger.LogError("User Id is empty", "Error when Block User");
                return Result.Failure("User Id is empty");
            }
            if (string.IsNullOrEmpty(UserToBlock))
            {
                logger.LogError("User to block Id is empty", "Error when Block User");
                return Result.Failure("User to block  Id is empty");
            }

            try
            {
                await _blockUserUnitOfWork.Blockuser(UserId, UserToBlock);
                return Result.Success();

            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message, "Error when Block User");
                return Result.Failure(ex.Message);
            }


        }
    }
}
