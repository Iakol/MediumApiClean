using ApiGateway.Application.DTO.Topic;
using ApiGateway.Application.Interface.TopicRepository;
using ApiGateway.Infrastructure.HttpService.HTTPRequestHelper;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace ApiGateway.Infrastructure.HttpService.TopicDomain
{
    public class TopicClient : ITopicRepository
    {
        private readonly string domainUrl;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TopicClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) 
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            domainUrl = Environment.GetEnvironmentVariable("TopicDomainURL").ToString();
        }

        public async Task<HttpResponseMessage> CreateTopic(Topic topic)
        {
            HttpContent content = JsonContent.Create(topic);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/CreateTopic", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> FindTopicDTOsByName(string Name)
        {
            HttpRequestMessage message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/Api/FindTopicDTOsByName", _httpContextAccessor, new Dictionary<string, string> 
            {
                {"name", Name}
            });
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> FindTopicsByName(string name)
        {        
            HttpRequestMessage message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/Api/FindTopicsByName", _httpContextAccessor, new Dictionary<string, string>
            {
                {"name", name}
            });

            return await _httpClient.SendAsync(message);

        }

        public async Task<HttpResponseMessage> GetLast100Topics()
        {
            HttpRequestMessage message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/Api/GetLast100Topics", _httpContextAccessor);
            return await _httpClient.SendAsync(message);

        }

        public async Task<HttpResponseMessage> GetTopic(string Id)
        {
            HttpRequestMessage message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/Api/GetTopic", _httpContextAccessor, new Dictionary<string, string>
            {
                {"Id",Id }
            });

            return await _httpClient.SendAsync(message);

        }

        public async Task<HttpResponseMessage> GetTopicDTOById(string Id)
        {
            HttpRequestMessage message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/Api/GetTopicDTOById", _httpContextAccessor, new Dictionary<string, string>
            {
                {"Id",Id }
            });
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> GetTopicDTOByIds(List<string> Ids)
        {           
            HttpContent content = JsonContent.Create(Ids);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/GetTopicDTOByIds", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);

        }

        public async Task<HttpResponseMessage> GetTopicsByIds(List<string> Ids)
        {            
            HttpContent content = JsonContent.Create(Ids);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/GetTopicsByIds", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }
    }
}
