using ReadingListDomain.Application.DTO;
using ReadingListDomain.Domain;

namespace ReadingListDomain.Application.UnitsOfWork
{
    public interface IUpdateReadingListUnit
    {
        public Task UpdateReadingList(ReadingList readingList);
    }
}
