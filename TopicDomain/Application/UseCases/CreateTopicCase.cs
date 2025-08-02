using Microsoft.Extensions.Logging;
using System.Data.Common;
using TopicDomain.Application.DTO;
using TopicDomain.Application.Interfaces;
using TopicDomain.Application.UnitOfWork;
using TopicDomain.Domain;
using TopicDomain.Infrastructure.Database.DBContext;
using TopicDomain.Presentation.UseCases;

namespace TopicDomain.Application.UseCases
{
    public class CreateTopicCase(ICreateTopiсUnitOfWork _createTopiсUnit, ILogger<CreateTopicCase> logger) : ICreateTopicCase
    {
        public async Task<Result> HandleAsync(Topic topic)
        {
            if (string.IsNullOrWhiteSpace(topic.Name))
            {
                logger.LogError("Name is Empty", "error when created Topic");
                return Result.Failure("Name is Empty");
            }
            try
            {
                await _createTopiсUnit.CreateTopicAsync(topic);
                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Db error when i created Topic");
                return Result.Failure(ex.Message);
            }

        }
    }
}
