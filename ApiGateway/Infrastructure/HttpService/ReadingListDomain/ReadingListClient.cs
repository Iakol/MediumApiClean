using ApiGateway.Application.DTO.ReadingList;
using ApiGateway.Infrastructure.HttpService.HTTPRequestHelper;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ApiGateway.Infrastructure.HttpService.ReadingListDomain
{
    public class ReadingListClient
    {
        private readonly string domainUrl;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ReadingListClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) 
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            domainUrl = Environment.GetEnvironmentVariable("ReadingListDomainURL").ToString();

        }

        public async Task<HttpResponseMessage> createReadingList( CreatePropsReadingListDTO readingList) 
        {
            HttpContent content = JsonContent.Create(readingList);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/createReadingList", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> DeleteReadingList(string readlingListId)
        {
            HttpContent content = JsonContent.Create(readlingListId);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/DeleteReadingList", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> GetListReadingListByCreatorId(string? UserId)
        {
            HttpRequestMessage message;
            if (UserId == null)
            {
                message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/Api/GetListReadingListByCreatorId", _httpContextAccessor);
                return await _httpClient.SendAsync(message);
            }
            else 
            {
                message = HttpRequestHelper.CreateGetRequest($"https://{domainUrl}/Api/GetListReadingListByCreatorId", _httpContextAccessor, new Dictionary<string, string>
                {
                {"UserId", UserId}
                });
                return await _httpClient.SendAsync(message);
            }
        }

        public async Task<HttpResponseMessage> GetListReadingListByIds([FromBody] List<string> Ids) 
        {
            HttpContent content = JsonContent.Create(Ids);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/GetListReadingListByIds", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> GetReadingList(string readingListId) 
        {
            HttpContent content = JsonContent.Create(readingListId);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/GetReadingList", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> SaveStoryToReadingList(SaveStoryPropsDTO saveStoryProps) 
        {
            HttpContent content = JsonContent.Create(saveStoryProps);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/SaveStoryToReadingList", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> UnSaveStoryFromReadingList( SaveStoryPropsDTO saveStoryProps) 
        {
            HttpContent content = JsonContent.Create(saveStoryProps);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/UnSaveStoryFromReadingList", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> UpdateNoteToSaveStoryInReadingList(SaveStoryPropsDTO saveStoryProps) 
        {
            HttpContent content = JsonContent.Create(saveStoryProps);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/UpdateNoteToSaveStoryInReadingList", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> UpdateReadingListCase(CreatePropsReadingListDTO UpdateCredReadingList) 
        {
            HttpContent content = JsonContent.Create(UpdateCredReadingList);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/UpdateReadingListCase", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> UpdateReadingListPrivate(UpdateReadingListPropsDTO readingListProps) 
        {
            HttpContent content = JsonContent.Create(readingListProps);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/UpdateReadingListPrivate", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);

        }

        public async Task<HttpResponseMessage> UpdateReadingListVisibleOfResponce(UpdateReadingListPropsDTO readingListProps) 
        {
            HttpContent content = JsonContent.Create(readingListProps);
            HttpRequestMessage message = HttpRequestHelper.CreatePostRequest($"https://{domainUrl}/Api/UpdateReadingListVisibleOfResponce", _httpContextAccessor, content);
            return await _httpClient.SendAsync(message);
        }

    }
}
