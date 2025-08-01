using ApiGateway.Infrastructure.HttpService.HTTPRequestHelper;
using System.Net.Http.Headers;

namespace ApiGateway.Infrastructure.HttpService.UserRelationship
{
    public class BlockClient
    {
        private string domainUrl;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public BlockClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            domainUrl = Environment.GetEnvironmentVariable("UserRealetionShipDomainURL").ToString();
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpResponseMessage> GetUserBlockList() 
        {            

            HttpRequestMessage message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/Block/GetUserBlockList", _httpContextAccessor);

            return await _httpClient.SendAsync(message);

        }

        public async Task<HttpResponseMessage> BlockUser(string blockId)
        {

            HttpContent content = JsonContent.Create(blockId);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Block/BlockUser", _httpContextAccessor, content);

            return await _httpClient.SendAsync(message);

        }

        public async Task<HttpResponseMessage> UnBlockUser(string blockId)
        {
            HttpContent content = JsonContent.Create(blockId);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Block/UnBlockUser", _httpContextAccessor, content);

            return await _httpClient.SendAsync(message);
        }
    }
}
