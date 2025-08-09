using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface ICreateConstantReadingListToUserCase
    {
        public Task<Result> Handle(string userId);
    }
}
