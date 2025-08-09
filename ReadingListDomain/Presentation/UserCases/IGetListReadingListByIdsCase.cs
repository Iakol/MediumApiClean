using ReadingListDomain.Application.DTO;
using ReadingListDomain.Domain;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IGetListReadingListByIdsCase
    {
        public Task<Result<List<ReadingList>>> Handle(List<string> Ids);
    }
}
