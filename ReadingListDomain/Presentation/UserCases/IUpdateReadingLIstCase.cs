using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IUpdateReadingLIstCase
    {
        public Task<Result> Handle(CreatePropsReadingListDTO newReadingList);

    }
}
