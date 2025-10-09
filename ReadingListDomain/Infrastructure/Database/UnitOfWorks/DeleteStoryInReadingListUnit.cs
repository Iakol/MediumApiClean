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

            await _storyInReadingListRepository.DeleteAsync(SaveStory);
            await _db.SaveChangesAsync();
        }
    }
}
