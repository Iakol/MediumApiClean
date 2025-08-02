using TopicDomain.Application.DTO;
using TopicDomain.Application.Interfaces;
using TopicDomain.Domain;
using TopicDomain.Presentation.UseCases;

namespace TopicDomain.Application.UseCases
{
    public class GetTopicDTOCase(ITopicRepository _topicRepository, ILogger<FindTopicsByNameCase> _logger) : IGetTopicDTOCase
    {
        public async Task<Result<Topic>> HandleAsync(string id)
        {
            try
            {
                Topic result = await _topicRepository.GetEntityAsync(id);
                if (result == null)
                {
                    _logger.LogError("Not found", "Error When Take Topic by id");
                    return Result<Topic>.Failure("Not found");
                }
                return Result<Topic>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error When Take Topic by id");
                return Result<Topic>.Failure(ex.Message);
            }
        }
    }
}
