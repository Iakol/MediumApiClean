using ApiGateway.Application.DTO.Topic;

namespace ApiGateway.Application.Interface.TopicRepository
{
    public interface ITopicRepository
    {
        public Task<HttpResponseMessage> CreateTopic(Topic topic);

        public Task<HttpResponseMessage> FindTopicsByName(string name);
        public Task<HttpResponseMessage> GetTopic(string Id);
        public Task<HttpResponseMessage> GetTopicsByIds(List<string> Ids);
        public Task<HttpResponseMessage> FindTopicDTOsByName(string Name);
        public Task<HttpResponseMessage> GetLast100Topics();
        public Task<HttpResponseMessage> GetTopicDTOByIds(List<string> topicids);
        public Task<HttpResponseMessage> GetTopicDTOById(string topicid);
    }
}
