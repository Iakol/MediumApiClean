using TopicDomain.Application.DTO;
using TopicDomain.Domain;

namespace TopicDomain.Presentation.UseCases
{
    public interface IGetTopicsByIdsCase
    {
        public Task<Result<List<Topic>>> HandleAsync(List<string> ids);
    }
}
