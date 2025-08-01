using System.Linq;
using TopicDomain.Application.DTO;
using TopicDomain.Application.Interfaces;
using TopicDomain.Domain;

namespace TopicDomain.Application.UseCases
{
    public class GetTopicsByIdsCase(ITopicRepository _topicRepository, ILogger<FindTopicsByNameCase> _logger)
    {

        public async Task<Result<List<Topic>>> HandleAsync(List<string> ids) 
        {
            if (ids.Count() == 0) 
            {
                _logger.LogError("Ids list is empty", "Error when take topic list by topic ids");
                return Result<List<Topic>>.Failure("Ids list is empty");


            }
            try
            {
                var tasks = ids.Select(async i => 
                {
                    return await _topicRepository.GetEntityAsync(i);
                }).ToList();
                List<Topic> topics = (await Task.WhenAll(tasks)).Where(t => t != null).ToList();
                return Result<List<Topic>>.Success(topics);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error when take topic list by topic ids");
                return Result<List<Topic>>.Failure(ex.Message);
            }
        }
    }
}
