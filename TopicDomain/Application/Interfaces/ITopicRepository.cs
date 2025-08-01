using TopicDomain.Domain;

namespace TopicDomain.Application.Interfaces
{
    public interface ITopicRepository
    {
        public Task AddAsync(Topic entity);
        public Task UpdateAsync(Topic entity);
        public Task DeleteAsync(string id);
        public Task<Topic?> GetEntityAsync(string id);

        public Task<List<Topic>> FindTopicsByNameAsync(string name);

        public Task<List<Topic>> GetLats100Topic();

    }
}
