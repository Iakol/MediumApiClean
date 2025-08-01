using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicDomain.Application.Interfaces;
using TopicDomain.Domain;
using TopicDomain.Infrastructure.Database.DBContext;
using TopicDomain.Infrastructure.Database.Models;

namespace TopicDomain.Infrastructure.Database.Repositories
{
    public class TopicRepository : CommonDbIteraction<TopicModel,Topic> , ITopicRepository
    {
        
        public TopicRepository(AppDBContext _db, IMapper _mapper) : base(_mapper,_db )
        {
        }

        public async Task<List<Topic>> FindTopicsByNameAsync(string name)
        {
            List<Topic> topics = await _db.Topics.Where(t => EF.Functions.Like(t.Name,$"%{name}%"))
                .Select(t => _mapper.Map<Topic>(t)).ToListAsync();
            return topics;
        }

        public Task<List<Topic>> GetLats100Topic()
        {
            return _db.Topics.OrderBy(t => t.CreatedAt).Take(100).Select(t => _mapper.Map<Topic>(t)).ToListAsync();
        }
    }
}
