using TopicDomain.Application.DTO;
using TopicDomain.Domain;

namespace TopicDomain.Presentation.UseCases
{
    public interface IGetTopicDTOCase
    {
        public Task<Result<Topic>> HandleAsync(string id);
    }
}
