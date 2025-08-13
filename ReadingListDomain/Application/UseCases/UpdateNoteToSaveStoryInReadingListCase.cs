using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.UnitOfWorks;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class UpdateNoteToSaveStoryInReadingListCase : IUpdateNoteToSaveStoryInReadingListCase
    {
        private ILogger<UpdateNoteToSaveStoryInReadingListCase> _logger;
        private IUpdateNoteForStoryInReadingListUnit _updateNoteForStoryInReadingList;
        private readonly IReadingListRepository _readingListRepository;

        public UpdateNoteToSaveStoryInReadingListCase(ILogger<UpdateNoteToSaveStoryInReadingListCase> logger, IUpdateNoteForStoryInReadingListUnit updateNoteForStoryInReadingList, IReadingListRepository readingListRepository)
        {
            _logger = logger;
            _updateNoteForStoryInReadingList = updateNoteForStoryInReadingList;
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

            try 
            {
                ReadingList readingList = await _readingListRepository.GetEntityAsync(ReadingListId);

                if (readingList != null)
                {
                    if (readingList.ReadingListCreator.Equals(userId))
                    {
                        await _updateNoteForStoryInReadingList.UpdateNote(SaveStoryId, Note);
                        return Result.Success();
                    }
                    else
                    {
                        _logger.LogError("User Is not Owner of list", "Error when update note story in List");
                        return Result.Failure("User Is not Owner of list");
                    }

                }
                else 
                {
                    _logger.LogError("ReadingList is not exist", "Error when update note story in List");
                    return Result.Failure("ReadingList is not exist");

                }

            } catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error when update note story in List");
                return Result.Failure(ex.Message);

            }



        }
    }
}
