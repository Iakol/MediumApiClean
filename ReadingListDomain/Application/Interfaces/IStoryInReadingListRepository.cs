using ReadingListDomain.Domain;

namespace ReadingListDomain.Application.Interfaces
{
    public interface IStoryInReadingListRepository
    {
        public Task AddAsync(StoryInReadingList entity);
        public Task UpdateAsync(StoryInReadingList entity);
        public Task DeleteAsync(string id);
        public Task<StoryInReadingList?> GetEntityAsync(string id);

        public Task<List<StoryInReadingList>> GetSaveStoryListByReadingList(string ReadingListId);


        public Task UpdateNote(string Note,string storyInListId);

        public Task DeleteAllStoryByReadingList(string ReadingListId);
    }
}
