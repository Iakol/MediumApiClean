using ReadingListDomain.Domain;

namespace ReadingListDomain.Application.UnitsOfWork
{
    public interface ICreateStoryInReadingListUnit
    {
        public Task CreateStoryInReadingList(StoryInReadingList Savestory);
    }
}
