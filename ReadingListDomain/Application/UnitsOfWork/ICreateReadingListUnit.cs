using ReadingListDomain.Domain;

namespace ReadingListDomain.Application.UnitsOfWork
{
    public interface ICreateReadingListUnit
    {
        public Task CreateReadingList(ReadingList readingListToCreate);
    }
}
