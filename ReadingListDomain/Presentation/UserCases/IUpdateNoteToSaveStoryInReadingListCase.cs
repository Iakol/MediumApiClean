using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IUpdateNoteToSaveStoryInReadingListCase
    {
        public Task<Result> Handle(string SaveStoryId, string userId, string ReadingListId,string Note);

    }
}
