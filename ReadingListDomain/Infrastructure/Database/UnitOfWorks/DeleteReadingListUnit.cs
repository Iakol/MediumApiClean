using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Infrastructure.Database.DBContext;

namespace ReadingListDomain.Infrastructure.Database.UnitsOfWork
{
    public class DeleteReadingListUnit(IReadingListRepository _readingListRepository, AppDBContext _db, IStoryInReadingListRepository _storyInReadingListRepository) : IDeleteReadingListUnit
    {
        public async Task DeleteReadingList(string readlingListId)
        {

            await _readingListRepository.DeleteAsync(readlingListId);
            await _storyInReadingListRepository.DeleteAllStoryByReadingList(readlingListId);
            await _db.SaveChangesAsync();
        }
    }
}
