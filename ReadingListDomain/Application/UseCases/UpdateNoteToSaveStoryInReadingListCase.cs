using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class UpdateNoteToSaveStoryInReadingListCase : IUpdateNoteToSaveStoryInReadingListCase
    {
        private ILogger<UpdateNoteToSaveStoryInReadingListCase> _logger;
        private IDeleteStoryInReadingListUnit _deleteStoryInReadingList;
        private readonly IReadingListRepository _readingListRepository;

        public UpdateNoteToSaveStoryInReadingListCase(ILogger<UpdateNoteToSaveStoryInReadingListCase> logger, IDeleteStoryInReadingListUnit deleteStoryInReadingList, IReadingListRepository readingListRepository)
        {
            _logger = logger;
            _deleteStoryInReadingList = deleteStoryInReadingList;
            _readingListRepository = readingListRepository;
        }
        public async Task<Result> Handle(string SaveStoryId, string userId, string ReadingListId, string Note)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User id us empty", "Error when update note story in List");
                return Result.Failure("User id is empty");
            }
            if (string.IsNullOrEmpty(ReadingListId))
            {
                _logger.LogError("ReadingList id us empty", "Error when update note story in List");
                return Result.Failure("ReadingList id is empty");
            }
            if (string.IsNullOrEmpty(SaveStoryId))
            {
                _logger.LogError("Save story id us empty", "Error when update note story in List");
                return Result.Failure("story id is empty");
            }



        }
    }
}
