using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.DBContext;

namespace ReadingListDomain.Infrastructure.Database.UnitOfWorks
{
    public class CreateReadingListUnit(IReadingListRepository _readingListRepository,AppDBContext _db) : ICreateReadingListUnit
    {
        public async Task CreateReadingList(ReadingList readingListToCreate)
        {
            if (readingListToCreate == null) 
            {
                throw new Exception("Reading List is Null");
            }
            await _readingListRepository.AddAsync(readingListToCreate);
            await _db.SaveChangesAsync();
        }
    }
}
