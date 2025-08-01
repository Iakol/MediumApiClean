using TopicDomain.Enum;

namespace TopicDomain.Domain
{
    public class Topic
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public TopicCreatorSourseEnum Sourse { get; set; }

        public Topic? Parent { get; set; }
        public List<Topic>? SubTopic { get; set; }

        public DateTime CreatedAt { get; set; }

        public Topic( string name, TopicCreatorSourseEnum sourse, Topic? parent = null, List<Topic>? subTopic = null)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Sourse = sourse;
            Parent = parent;
            SubTopic = subTopic;
            CreatedAt = DateTime.Now;
        }
    }
}
