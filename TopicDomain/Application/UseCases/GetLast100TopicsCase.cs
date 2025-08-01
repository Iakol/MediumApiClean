using AutoMapper;
using Microsoft.Extensions.Logging;
using TopicDomain.Application.DTO;
using TopicDomain.Application.Interfaces;
using TopicDomain.Domain;

namespace TopicDomain.Application.UseCases
{
    public class GetLast100TopicsCase(ITopicRepository _topicRepository, ILogger<FindTopicsByNameCase> _logger )
    {
        public async Task<Result<List<Topic>>> HandleAsync() 
        {
            try
            {

                return Result<List<Topic>>.Success(await _topicRepository.GetLats100Topic());
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error when a take 100 last topics");
                return Result<List<Topic>>.Failure(ex.Message);
            }
        
        }
    }
}
