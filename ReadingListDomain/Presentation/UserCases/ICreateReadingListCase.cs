using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface ICreateReadingListCase
    {
        public Task<Result> Handle(CreatePropsReadingListDTO newReadingList, string userId);
    }
}
