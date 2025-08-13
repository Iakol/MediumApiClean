using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.DBContext;

namespace ReadingListDomain.Infrastructure.Database.UnitOfWorks
{
    public class UpdateReadingListVisibleOfResponceUnit(IReadingListRepository _readingListRepository, AppDBContext _db) : IUpdateReadingListVisibleOfResponceUnit
    {
        public async Task SetVisibleOfResponce(bool isVisible, string readingListId)
        {
            ReadingList readingList = await _readingListRepository.GetEntityAsync(readingListId);
            if (readingList == null) 
            {
                throw new Exception("ReadingList is not exist");
            }

            await _readingListRepository.UpdateReadingLisOpenResponces(isVisible, readingListId);
            await _db.SaveChangesAsync();
        }
    }
}
