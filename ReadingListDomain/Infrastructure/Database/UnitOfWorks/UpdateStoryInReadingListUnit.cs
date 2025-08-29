using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Application.UseCases;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.DBContext;

namespace ReadingListDomain.Infrastructure.Database.UnitOfWorks
{
    public class UpdateStoryInReadingListUnit(IStoryInReadingListRepository _storyInReadingListRepository, AppDBContext _db) : IUpdateStoryInReadingListUnit
    {

        public async Task UpdateSavedStory(StoryInReadingList story)
        {
            
            await _storyInReadingListRepository.UpdateAsync(story);
            await _db.SaveChangesAsync();   
        }
    }
}
