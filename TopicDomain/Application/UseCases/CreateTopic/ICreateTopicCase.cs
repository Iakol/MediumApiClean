using TopicDomain.Application.DTO;
using TopicDomain.Domain;

namespace TopicDomain.Application.UseCases.CreateTopic
{
    public interface ICreateTopicCase
    {
        public Task<Result> HandleAsync(Topic topic);
    }
}
