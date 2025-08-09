using System.Data.SqlTypes;

namespace ReadingListDomain.Application.DTO
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }

        public static Result Success() => new(true, null);
        public static Result Failure(string error) => new(false, error);

        protected Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }


    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public static Result<T> Success(T data) => new(true, null, data);
        public static Result<T> Failure(string error) => new(false, error,default);

        private Result(bool isSuccess, string? error, T? data) : base(isSuccess, error)
        {
            Data = data;
        }
    }
}
