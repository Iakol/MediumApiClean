using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IUnSaveStoryFromReadingListCase
    {
        public Task<Result> Handle(string SaveStoryId, string userId, string ReadingListId);

    }
}
