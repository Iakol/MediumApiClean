using TopicDomain.Domain;
using TopicDomain.Enum;
using TopicDomain.Infrastructure.Database.Repositories;

namespace TopicDomain.Infrastructure.Database.Models
{
    public class TopicModel : IDomainEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public TopicCreatorSourseEnum Sourse { get; set; }

        public TopicModel? Parent { get; set; }
        public string? ParentId { get; set; }

        public List<TopicModel> SubTopic { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
