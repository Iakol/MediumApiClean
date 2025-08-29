using ReadingListDomain.Domain;

namespace ReadingListDomain.Application.UnitsOfWork
{
    public interface IUpdateStoryInReadingListUnit
    {
        public Task UpdateSavedStory(StoryInReadingList story);
    }
}
