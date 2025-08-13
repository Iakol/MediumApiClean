using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IUpdateReadingListCase
    {
        public Task<Result> Handle(CreatePropsReadingListDTO newReadingList, string userId);

    }
}
