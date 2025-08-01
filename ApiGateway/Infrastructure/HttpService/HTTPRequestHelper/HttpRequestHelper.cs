using System;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;

namespace ApiGateway.Infrastructure.HttpService.HTTPRequestHelper
{
    public static class HttpRequestHelper
    {
        public static HttpRequestMessage CreatePostRequest( string url, IHttpContextAccessor _httpContextAccessor, HttpContent content) 
        {
            
            HttpRequestMessage message =  new HttpRequestMessage(HttpMethod.Post, url) 
            {
                Content = content
            };
            string token = GetAuthToken(_httpContextAccessor);

            if (!string.IsNullOrEmpty(token)) 
            {
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return message;
        }


        public static HttpRequestMessage CreateGetRequest(string url, IHttpContextAccessor _httpContextAccessor, Dictionary<string, string> query = null)
        {

            if (query != null && query.Count() != 0) 
            {
                url = CreateUrlWithParamsForRequest(url, query);
            }
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);
            string token = GetAuthToken(_httpContextAccessor);
            if (!string.IsNullOrEmpty(token))
            {
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return message;
        }

        private static string? GetAuthToken(IHttpContextAccessor _httpContextAccessor) 
        {
            string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            string token = string.Empty;
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                return authorizationHeader.Substring("Bearer ".Length).Trim();
            }

            return token;
        }

        private static string CreateUrlWithParamsForRequest(string url ,Dictionary<string, string> query) 
        {
            url = QueryHelpers.AddQueryString(url,query);
            return url;
        }
    }
}
