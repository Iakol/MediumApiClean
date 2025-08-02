using TopicDomain.Application.DTO;

namespace TopicDomain.Presentation.UseCases
{
    public interface IGetTopicDTOByIdsCase
    {
        public Task<Result<List<TopicDTO>>> HandleAsync(List<string> ids);
    }
}
