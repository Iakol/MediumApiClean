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
        private IUpdateStoryInReadingListUnit _updateStoryInReadingList;
        private IStoryInReadingListRepository _storyInReadingListRepository;

        private readonly IReadingListRepository _readingListRepository;

        public UpdateNoteToSaveStoryInReadingListCase(ILogger<UpdateNoteToSaveStoryInReadingListCase> logger, IUpdateStoryInReadingListUnit updateStoryInReadingList, IReadingListRepository readingListRepository, IStoryInReadingListRepository storyInReadingListRepository)
        {
            _logger = logger;
            _updateStoryInReadingList = updateStoryInReadingList;
            _readingListRepository = readingListRepository;
            _storyInReadingListRepository = storyInReadingListRepository;
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
                        StoryInReadingList story = await _storyInReadingListRepository.GetEntityAsync(SaveStoryId);
                        if (story != null) 
                        {
                            story.Note = Note;
                             
                            await _updateStoryInReadingList.UpdateSavedStory(story);
                            return Result.Success();
                        }
                        else
                        {
                            _logger.LogError("Saved story not exist", "Error when update note story in List");
                            return Result.Failure("Saved story not exist");
                        }

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
