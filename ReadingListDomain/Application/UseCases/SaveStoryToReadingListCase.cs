using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.Repositories;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class SaveStoryToReadingListCase : ISaveStoryToReadingListCase
    {
        private ILogger<SaveStoryToReadingListCase> _logger;
        private ICreateStoryInReadingListUnit _createStoryInReadingListUnit;
        private readonly IReadingListRepository _readingListRepository;
        public SaveStoryToReadingListCase(ILogger<SaveStoryToReadingListCase> logger, ICreateStoryInReadingListUnit createStoryInReadingListUnit,IReadingListRepository readingListRepository)
        {
            _logger = logger;
            _createStoryInReadingListUnit = createStoryInReadingListUnit;
            _readingListRepository = readingListRepository;
        }

        public async Task<Result> Handle(string storyId, string userId, string ReadingListId)
        {
            if (string.IsNullOrEmpty(userId)) 
            {
                _logger.LogError("User id us empty", "Error whe create Save story in List");
                return Result.Failure("User id is empty");
            }
            if (string.IsNullOrEmpty(ReadingListId))
            {
                _logger.LogError("ReadingList id us empty", "Error whe create Save story in List");
                return Result.Failure("ReadingList id is empty");
            }
            if (string.IsNullOrEmpty(storyId))
            {
                _logger.LogError("story id us empty", "Error whe create Save story in List");
                return Result.Failure("story id is empty");
            }


            try 
            {
                ReadingList list = await _readingListRepository.GetEntityAsync(ReadingListId);
                if (list != null)
                {
                    if (list.ReadingListCreator.Equals(userId))
                    {
                        StoryInReadingList storyInList = new StoryInReadingList(storyId, ReadingListId);
                        await _createStoryInReadingListUnit.CreateStoryInReadingList(storyInList);
                        return Result.Success();
                    }
                    else
                    {
                        _logger.LogError("The user is not owner of current reading list", "Error whe create Save story in List");
                        return Result.Failure("The user is not owner of current reading list");
                    }
                }
                else
                {
                    _logger.LogError("The reading list is not exist", "Error whe create Save story in List");
                    return Result.Failure("The reading list is not exist");
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error whe create Save story in List");
                return Result.Failure(ex.Message);
            }
        }
    }
}
