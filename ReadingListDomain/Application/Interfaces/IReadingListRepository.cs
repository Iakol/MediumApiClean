using ReadingListDomain.Domain;

namespace ReadingListDomain.Application.Interfaces
{
    public interface IReadingListRepository
    {
        public Task AddAsync(ReadingList entity);
        public Task UpdateAsync(ReadingList entity);
        public Task DeleteAsync(string id);
        public Task<ReadingList?> GetEntityAsync(string id);

        public Task<List<ReadingList>> GetListOfReadingList(List<string> Ids);
        public Task<List<ReadingList>> GetListOfReadingList(string UserId);
        public Task UpdateReadingListPrivate(bool isPrivate,string readingListID);
        public Task UpdateReadingLisOpenResponces(bool isOpenResponces, string readingListID);


    }
}
