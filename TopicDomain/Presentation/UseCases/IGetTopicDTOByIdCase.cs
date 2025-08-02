using TopicDomain.Application.DTO;

namespace TopicDomain.Presentation.UseCases
{
    public interface IGetTopicDTOByIdCase
    {
        public Task<Result<TopicDTO>> HandleAsync(string id);
    }
}
