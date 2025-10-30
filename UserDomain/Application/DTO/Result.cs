using System.Text.Json;

namespace UserDomain.Application.DTO
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }

        protected Result(bool IsSuccess, string Error)
        {
            this.IsSuccess = IsSuccess;
            this.Error = Error;
        }

        public static Result Success() => new(true, null);


        public static Result Failure(string error) => new(false, error);

    }

    public class Result<T> : Result
    {
        protected Result(bool IsSuccess, string Error, T? data) : base(IsSuccess, Error)
        {
            this.data = data;
        }

        public static Result<T> Success(T data) => new(true, null, data);


        public static Result<T> Failure(string error) => new(false, error, default);

        public T? data { get; set; }
    }
}
