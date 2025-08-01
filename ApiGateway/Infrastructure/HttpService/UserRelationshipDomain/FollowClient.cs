using ApiGateway.Infrastructure.HttpService.HTTPRequestHelper;

namespace ApiGateway.Infrastructure.HttpService.UserRelationship
{
    public class FollowClient
    {
        private string domainUrl = Environment.GetEnvironmentVariable("UserRealetionShipDomainURL").ToString();
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FollowClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            domainUrl = Environment.GetEnvironmentVariable("UserRealetionShipDomainURL").ToString();
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpResponseMessage> GetUserFollowList()
        {
            HttpRequestMessage message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/Follow/GetUserFollowList", _httpContextAccessor);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> FollowUser(string FollowId)
        {
            HttpContent content = JsonContent.Create(FollowId);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Follow/FollowUser", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> UnFollowUser(string UnFollowUser)
        {
            HttpContent content = JsonContent.Create(UnFollowUser);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Follow/UnFollowUser", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> GetFollowCount()
        {
            HttpRequestMessage message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/Follow/GetFollowCount", _httpContextAccessor);
            return await _httpClient.SendAsync(message);
        }


    }
}
