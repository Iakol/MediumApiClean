using AutoMapper;
using TopicDomain.Application.DTO;
using TopicDomain.Application.Interfaces;
using TopicDomain.Domain;
using TopicDomain.Presentation.UseCases;

namespace TopicDomain.Application.UseCases
{
    public class GetTopicDTOByIdsCase(ITopicRepository _topicRepository, ILogger<FindTopicsByNameCase> _logger, IMapper _mapper) : IGetTopicDTOByIdsCase
    {
        public async Task<Result<List<TopicDTO>>> HandleAsync(List<string> ids)
        {
            if (ids.Count() == 0)
            {
                _logger.LogError("Ids list is empty", "Error when take topic list by topic ids");
                return Result<List<TopicDTO>>.Failure("Ids list is empty");


            }
            try
            {
                var tasks = ids.Select(async i =>
                {
                    return await _topicRepository.GetEntityAsync(i);
                }).ToList();
                List<Topic> topics = (await Task.WhenAll(tasks)).Where(t => t != null).ToList();
                List<TopicDTO> topicDTOs = topics.Select(t => _mapper.Map<TopicDTO>(t)).ToList();
                return Result<List<TopicDTO>>.Success(topicDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error when take topic list by topic ids");
                return Result<List<TopicDTO>>.Failure(ex.Message);
            }
        }
    }
}
