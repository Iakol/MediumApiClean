using ApiGateway.Infrastructure.HttpService.HTTPRequestHelper;
using System.Net.Http;

namespace ApiGateway.Infrastructure.HttpService.UserRelationship
{
    public class MuteClient
    {
        private string domainUrl;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public MuteClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            domainUrl = Environment.GetEnvironmentVariable("UserRealetionShipDomainURL").ToString();
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<HttpResponseMessage> GetUserMuteList()
        {

            HttpRequestMessage message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/Mute/GetUserMuteList", _httpContextAccessor);
            return await _httpClient.SendAsync(message);

        }

        public async Task<HttpResponseMessage> MuteUser(string MuteId)
        {

            HttpContent content = JsonContent.Create(MuteId);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Mute/MuteUser", _httpContextAccessor, content);

            return await _httpClient.SendAsync(message);

        }

        public async Task<HttpResponseMessage> UnMuteUser(string MuteId)
        {
            HttpContent content = JsonContent.Create(MuteId);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Mute/UnMuteUser", _httpContextAccessor, content);

            return await _httpClient.SendAsync(message);
        }

    }
}
