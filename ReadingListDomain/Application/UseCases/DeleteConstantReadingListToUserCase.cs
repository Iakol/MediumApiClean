using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class DeleteConstantReadingListToUserCase(IDeleteReadingListUnit _deleteReadingListUnit,ILogger<DeleteConstantReadingListToUserCase> _logger, IReadingListRepository _readingListRepository) : IDeleteConstantReadingListToUserCase
    {

        public async Task<Result> Handle(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                _logger.LogError("User id is empty", "Error when Delete Constant ReadingList");
                return Result.Failure("User id is empty");
            }

            try
            {
                ReadingList readingList = await _readingListRepository.GetConstantListByUserIdIfExist(userId);
                if (readingList == null)
                {
                    return Result.Success();
                }
                
                await _deleteReadingListUnit.DeleteReadingList(readingList.Id);
                return Result.Success();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error when delete ReadingList");
                return Result.Failure(ex.Message);
            }
        }
    }
}
