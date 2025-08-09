using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Application.UnitsOfWork
{
    public interface IUpdateReadingLIstUnit
    {
        public Task UpdateReadingList(CreatePropsReadingListDTO readingList);
    }
}
