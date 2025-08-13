using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.DBContext;

namespace ReadingListDomain.Infrastructure.Database.UnitOfWorks
{
    public class UpdateReadingListUnit(IReadingListRepository _readingListRepository, AppDBContext _db) : IUpdateReadingListUnit
    {

        public async Task UpdateReadingList(ReadingList readingList)
        {
            await _readingListRepository.UpdateAsync(readingList);
            await _db.SaveChangesAsync();

        }
    }
}
