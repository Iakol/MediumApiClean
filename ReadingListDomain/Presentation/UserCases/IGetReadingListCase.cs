using ReadingListDomain.Application.DTO;
using ReadingListDomain.Domain;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IGetReadingListCase
    {
        public Task<Result<ReadingList>> Handle(string readingListId);
    }
}
