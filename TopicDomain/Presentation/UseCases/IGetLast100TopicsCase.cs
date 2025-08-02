using TopicDomain.Application.DTO;
using TopicDomain.Domain;

namespace TopicDomain.Presentation.UseCases
{
    public interface IGetLast100TopicsCase
    {
        public Task<Result<List<Topic>>> HandleAsync();
    }
}
