using TopicDomain.Application.DTO;

namespace TopicDomain.Presentation.UseCases
{
    public interface IFindTopicsDTOByNameCase
    {
        public Task<Result<List<TopicDTO>>> HandleAsync(string name);
    }
}
