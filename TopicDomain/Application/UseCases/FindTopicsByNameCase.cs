using TopicDomain.Application.DTO;
using TopicDomain.Application.Interfaces;
using TopicDomain.Domain;

namespace TopicDomain.Application.UseCases
{
    public class FindTopicsByNameCase(ITopicRepository _topicRepository, ILogger<FindTopicsByNameCase> _logger)
    {
        public async Task<Result<List<Topic>>> HandleAsync(string name) 
        {
            if (string.IsNullOrWhiteSpace(name)) 
            {
                _logger.LogError("Topic name is empty", "Error when recive topics by Name");
                return Result<List<Topic>>.Failure("Topic name is empty");
            }
            try
            {
                List<Topic> topics = await _topicRepository.FindTopicsByNameAsync(name);
                return Result<List<Topic>>.Success(topics);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error when recive topics by Name");
                return Result<List<Topic>>.Failure(ex.Message);
            }

        }
    }
}
