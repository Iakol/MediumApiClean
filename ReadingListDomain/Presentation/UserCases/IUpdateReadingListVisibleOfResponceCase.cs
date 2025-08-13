using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IUpdateReadingListVisibleOfResponceCase
    {
        public Task<Result> Handle(bool isVisible, string userId, string ReadingListId);

    }
}
