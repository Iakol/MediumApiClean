using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.ISaveReadingListUnitOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;
using UserRealetionShipDomain.Presentation.UseCases.ReadingListCases;

namespace UserRealetionShipDomain.Application.UseCases.SaveReadingListCases
{
    public class SaveReadingListCase(ISaveReadingListUnitOfWork saveReadingListUnitOfWork, ILogger<SaveReadingListCase> _logger, AppDbContext _db) : ISaveReadingListCase
    {
        public async Task<Result> Handle(string UserId, string readingList) 
        {
            if (string.IsNullOrWhiteSpace(UserId)) 
            {
                _logger.LogError("userId is empty", "Error when save readingList");
                return Result.Failure("userId is empty");

            }
            if (string.IsNullOrWhiteSpace(readingList))
            {
                _logger.LogError("readingListID is empty", "Error when save readingList");
                return Result.Failure("readingListID is empty");
            }

            try
            {
                await saveReadingListUnitOfWork.SaveReadinglist(UserId, readingList);
                return Result.Success();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error when save readingList");
                return Result.Failure(ex.Message);
            }
            
        }
    }
}
