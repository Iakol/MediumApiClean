using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.DBContext;
using ReadingListDomain.Infrastructure.Database.Models;

namespace ReadingListDomain.Infrastructure.Database.UnitOfWorks
{
    public class DeleteStoryInReadingListUnit(IStoryInReadingListRepository _storyInReadingListRepository, AppDBContext _db) : IDeleteStoryInReadingListUnit
    {
        public async Task DeleteSaveStoryInReadingList(string SaveStory)
        {
            StoryInReadingList story = await _storyInReadingListRepository.GetEntityAsync(SaveStory);
            if (story == null)
            { 
                throw new Exception("The save story is not exist");
            }

            await _storyInReadingListRepository.DeleteAsync(SaveStory);
            await _db.SaveChangesAsync();



        }
    }
}
