using Microsoft.Extensions.Logging;
using System.Data.Common;
using TopicDomain.Application.Interfaces;
using TopicDomain.Application.UnitOfWork;
using TopicDomain.Domain;
using TopicDomain.Infrastructure.Database.DBContext;

namespace TopicDomain.Infrastructure.Database.UnitsOfWork
{
    public class CreateTopiсUnitOfWork(ITopicRepository _topicRepository, AppDBContext _db) : ICreateTopiсUnitOfWork
    {
        public async Task CreateTopicAsync(Topic topic)
        {
            await _topicRepository.AddAsync(topic);
            await _db.SaveChangesAsync();
            
        }
    }
}
