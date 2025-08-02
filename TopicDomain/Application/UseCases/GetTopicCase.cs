using TopicDomain.Application.DTO;
using TopicDomain.Application.Interfaces;
using TopicDomain.Domain;
using TopicDomain.Presentation.UseCases;

namespace TopicDomain.Application.UseCases
{
    public class GetTopicCase(ITopicRepository _topicRepository, ILogger<GetTopicCase> _logger) : IGetTopicCase
    {
        public async Task<Result<Topic>> HandleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) 
            {
                _logger.LogError("Topic Id is null", "Error When Take Topic by id");
                return Result<Topic>.Failure("Topic Id is null");
            }
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
