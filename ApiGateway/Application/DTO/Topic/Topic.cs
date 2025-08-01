namespace ApiGateway.Application.DTO.Topic
{
    public class Topic
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public TopicCreatorSourseEnum Sourse { get; set; }

        public Topic? Parent { get; set; }
        public List<Topic>? SubTopic { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
