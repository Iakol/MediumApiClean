using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IUpdateReadingListVisibleOfResponceCase
    {
        public Task<Result> Handle(bool isPrivate, string userId, string ReadingListId);

    }
}
