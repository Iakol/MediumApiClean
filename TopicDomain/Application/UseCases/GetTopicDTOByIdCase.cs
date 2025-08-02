using AutoMapper;
using TopicDomain.Application.DTO;
using TopicDomain.Application.Interfaces;
using TopicDomain.Domain;
using TopicDomain.Presentation.UseCases;

namespace TopicDomain.Application.UseCases
{
    public class GetTopicDTOByIdCase(ITopicRepository _topicRepository, ILogger<GetTopicDTOByIdCase> _logger, IMapper _mapper) : IGetTopicDTOByIdCase
    {
        public async Task<Result<TopicDTO>> HandleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Topic Id is null", "Error When Take Topic by id");
                return Result<TopicDTO>.Failure("Topic Id is null");
            }
            try
            {
                Topic result = await _topicRepository.GetEntityAsync(id);
                if (result == null)
                {
                    _logger.LogError("Not found", "Error When Take Topic by id");
                    return Result<TopicDTO>.Failure("Not found");
                }

                return Result<TopicDTO>.Success(_mapper.Map<TopicDTO>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error When Take Topic by id");
                return Result<TopicDTO>.Failure(ex.Message);
            }
        }
    }
}
