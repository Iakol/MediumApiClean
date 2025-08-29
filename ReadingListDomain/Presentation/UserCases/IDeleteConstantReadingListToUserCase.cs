using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IDeleteConstantReadingListToUserCase
    {
        public Task<Result> Handle(string userId);
    }
}
