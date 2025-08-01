using ApiGateway.Infrastructure.HttpService.HTTPRequestHelper;

namespace ApiGateway.Infrastructure.HttpService.UserRelationship
{
    public class SaveReadingListClient
    {
        private string domainUrl;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public SaveReadingListClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            domainUrl = Environment.GetEnvironmentVariable("UserRealetionShipDomainURL").ToString();
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpResponseMessage> GetUserSaveReadingList(string? userId)
        {

            HttpRequestMessage message;

            if (string.IsNullOrEmpty(userId))
            {
                message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/ReadingList/GetUserSaveReadingList", _httpContextAccessor);
            }
            else 
            {
                message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/ReadingList/GetUserSaveReadingList", _httpContextAccessor,new Dictionary<string, string> 
                {
                    { "userId",userId}
                });
            }

            return await _httpClient.SendAsync(message);

        }

        public async Task<HttpResponseMessage> SaveReadingList(string ReadingList)
        {

            HttpContent content = JsonContent.Create(ReadingList);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/ReadingList/SaveReadingList", _httpContextAccessor, content);

            return await _httpClient.SendAsync(message);

        }

        public async Task<HttpResponseMessage> UnSaveReadingList(string ReadingList)
        {
            HttpContent content = JsonContent.Create(ReadingList);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/ReadingList/UnSaveReadingList", _httpContextAccessor, content);

            return await _httpClient.SendAsync(message);
        }
    }
}
