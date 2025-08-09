using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Infrastructure.Database.DBContext;

namespace ReadingListDomain.Infrastructure.Database.UnitsOfWork
{
    public class DeleteReadingListUnit(IReadingListRepository _readingListRepository, AppDBContext _db) : IDeleteReadingListUnit
    {
        public async Task DeleteReadingList(string readlingListId)
        {
            if (string.IsNullOrWhiteSpace(readlingListId)) 
            {
                throw new Exception("Reading List id is empty");
            }
            await _readingListRepository.DeleteAsync(readlingListId);
            await _db.SaveChangesAsync();
        }
    }
}
