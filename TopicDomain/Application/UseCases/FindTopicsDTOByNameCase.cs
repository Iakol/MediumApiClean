using AutoMapper;
using System.Collections.Generic;
using TopicDomain.Application.DTO;
using TopicDomain.Application.Interfaces;
using TopicDomain.Domain;

namespace TopicDomain.Application.UseCases
{
    public class FindTopicsDTOByNameCase(ITopicRepository _topicRepository, ILogger<FindTopicsByNameCase> _logger, IMapper _mapper)
    {
        public async Task<Result<List<TopicDTO>>> HandleAsync(string name) 
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogError("Topic name is empty", "Error when recive topics by Name");
                return Result<List<TopicDTO>>.Failure("Topic name is empty");
            }
            try
            {
                List<Topic> topics = await _topicRepository.FindTopicsByNameAsync(name);

                List<TopicDTO> topicsDto =  topics.Select(t => _mapper.Map<TopicDTO>(t)).ToList();
                return Result<List<TopicDTO>>.Success(topicsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error when recive topics by Name");
                return Result<List<TopicDTO>>.Failure(ex.Message);
            }

        }
    }
}
