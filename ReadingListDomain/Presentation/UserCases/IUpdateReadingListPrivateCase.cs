using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IUpdateReadingListPrivateCase
    {
        public Task<Result> Handle(bool isPrivate, string userId, string ReadingListId);
    }
}
