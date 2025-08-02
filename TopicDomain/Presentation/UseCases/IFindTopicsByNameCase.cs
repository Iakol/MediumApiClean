using TopicDomain.Application.DTO;
using TopicDomain.Domain;

namespace TopicDomain.Presentation.UseCases
{
    public interface IFindTopicsByNameCase
    {
        public Task<Result<List<Topic>>> HandleAsync(string name);
    }
}
