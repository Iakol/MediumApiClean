using ReadingListDomain.Domain;

namespace ReadingListDomain.Application.UnitsOfWork
{
    public interface IDeleteReadingListUnit
    {
        public Task DeleteReadingList(string readlingListId);

    }
}
