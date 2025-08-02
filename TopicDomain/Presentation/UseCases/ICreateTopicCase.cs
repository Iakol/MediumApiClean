using TopicDomain.Application.DTO;
using TopicDomain.Domain;

namespace TopicDomain.Presentation.UseCases
{
    public interface ICreateTopicCase
    {
        public Task<Result> HandleAsync(Topic topic);
    }
}
