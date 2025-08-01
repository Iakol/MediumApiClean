using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRealetionShipDomain.Application.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserRealetionShipDomain.Application.Helper
{
 
    public static class HttpMessageHelper 
    {
        public static HttpMessage<T> Success<T>(T data, string message) 
        {
            return new HttpMessage<T>
            {
                data = data,
                message = message,
                sucsses = true
            };
        }
        public static HttpMessage Success( string message) 
        {
            return new HttpMessage
            {
                message = message,
                sucsses = true
            };
        }

        public static HttpMessage<T> Failure<T>(T data, string message)
        {
            return new HttpMessage<T>
            {
                message = message,
                sucsses = false
            };
        }
        public static HttpMessage Failure(string message)
        {
            return new HttpMessage
            {
                message = message,
                sucsses = false
            };
        }


    }
}
