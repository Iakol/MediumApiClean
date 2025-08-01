using TopicDomain.Domain;

namespace TopicDomain.Application.UnitOfWork
{
    public interface ICreateTopiсUnitOfWork
    {
        public Task CreateTopicAsync(Topic topic);
    }
}
