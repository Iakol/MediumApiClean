using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface ISaveStoryToReadingListCase
    {
        public Task<Result> Handle(string storyId, string userId, string ReadingListId);

    }
}
