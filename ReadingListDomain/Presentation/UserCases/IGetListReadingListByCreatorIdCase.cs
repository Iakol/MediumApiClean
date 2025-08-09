using ReadingListDomain.Application.DTO;
using ReadingListDomain.Domain;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IGetListReadingListByCreatorIdCase
    {
        public Task<Result<List<ReadingList>>> Handle(string userId);
    }
}
