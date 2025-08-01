using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.ISaveReadingListUnitOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.ReadingListCases;

namespace UserRealetionShipDomain.Application.UseCases.SaveReadingListCases
{
    public class UnSaveReadingListCase(IUnSaveReadingListUnitOfWork _saveReadingListUnitOfWork,ILogger<UnSaveReadingListCase> logger) : IUnSaveReadingListCase
    {
        public async Task<Result> Handle(string UserId, string readingList)
        {
            if (string.IsNullOrEmpty(readingList)) 
            {
                logger.LogError("Reading list Id is empty", "Error when UnSave List");
                return Result.Failure("Reading list Id is empty");
            }
            if (string.IsNullOrEmpty(UserId)) 
            {
                logger.LogError("User Id is empty", "Error when UnSave List");
                return Result.Failure("User Id is empty");

            }

            try
            {
                await _saveReadingListUnitOfWork.UnSaveReadingList( UserId, readingList);
                return Result.Success();
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message, "Error when UnSave List");
                return Result.Failure(ex.Message);
            }

            
        }
    }
}
