using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.DBContext;

namespace ReadingListDomain.Infrastructure.Database.UnitOfWorks
{
    public class UpdateReadingListPrivateUnit(IReadingListRepository _readingListRepository, AppDBContext _db) : IUpdateReadingListPrivateUnit
    {
        public async Task SetPrivate(bool isPrivate, string ReadingListId)
        {
            ReadingList readinglist = await _readingListRepository.GetEntityAsync(ReadingListId);
            if (readinglist == null) 
            {
                throw new Exception("ReadingList is not found");
            }
            await _readingListRepository.UpdateReadingListPrivate(isPrivate, ReadingListId);
            await _db.SaveChangesAsync();
        }
    }
}
