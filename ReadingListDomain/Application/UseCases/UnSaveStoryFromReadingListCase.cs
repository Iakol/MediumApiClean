using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.Repositories;
using ReadingListDomain.Infrastructure.Database.UnitOfWorks;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class UnSaveStoryFromReadingListCase : IUnSaveStoryFromReadingListCase
    {
        private ILogger<UnSaveStoryFromReadingListCase> _logger;
        private IDeleteStoryInReadingListUnit _deleteStoryInReadingList;
        private readonly IReadingListRepository _readingListRepository;

        public UnSaveStoryFromReadingListCase(ILogger<UnSaveStoryFromReadingListCase> logger, IDeleteStoryInReadingListUnit deleteStoryInReadingList, IReadingListRepository readingListRepository)
        {
            _logger = logger;
            _deleteStoryInReadingList = deleteStoryInReadingList;
            _readingListRepository = readingListRepository;
        }
        public async Task<Result> Handle(string SaveStoryId, string userId, string ReadingListId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User id us empty", "Error when unSave story in List");
                return Result.Failure("User id is empty");
            }
            if (string.IsNullOrEmpty(ReadingListId))
            {
                _logger.LogError("ReadingList id us empty", "Error when unSave story in List");
                return Result.Failure("ReadingList id is empty");
            }
            if (string.IsNullOrEmpty(SaveStoryId))
            {
                _logger.LogError("Save story id us empty", "Error when unSave story in List");
                return Result.Failure("story id is empty");
            }

            try
            {

                ReadingList readingList = await _readingListRepository.GetEntityAsync(ReadingListId);

                if (readingList != null)
                {
                    if (readingList.ReadingListCreator.Equals(userId))
                    {
                        await _deleteStoryInReadingList.DeleteSaveStoryInReadingList(SaveStoryId);
                        return Result.Success();
                    }
                    else
                    {
                        _logger.LogError("The user not owner of Reading List", "Error when create unSave story in List");
                        return Result.Failure("The user not owner of Reading List");
                    }

                }
                else 
                {
                    _logger.LogError("The Reading list where saved story is not exist", "Error when unSave story in List");
                    return Result.Failure("The Reading list where saved story is not exist");

                }

            } catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error when unSave story in List");
                return Result.Failure(ex.Message);
            }


        }
    }
}
