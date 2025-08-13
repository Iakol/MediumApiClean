using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Application.UseCases;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.DBContext;

namespace ReadingListDomain.Infrastructure.Database.UnitOfWorks
{
    public class UpdateNoteForStoryInReadingListUnit(IStoryInReadingListRepository _storyInReadingListRepository, AppDBContext _db) : IUpdateNoteForStoryInReadingListUnit
    {

        public async Task UpdateNote(string SaveStoryId, string NewNote)
        {
            StoryInReadingList story = await _storyInReadingListRepository.GetEntityAsync(SaveStoryId);
            if (story == null) 
            {
                throw new Exception("Save story is not exist");
            }
            await _storyInReadingListRepository.UpdateNote(NewNote, SaveStoryId);
            await _db.SaveChangesAsync();   
        }
    }
}
