namespace Test.Application.Common.Models
{
    public class Result<T>
    {
        public Result(T data, string message = "")
        {
            Data = data;
            Message = message;
        }

        public T Data { get; init; }

        public string Message { get; init; }
    }
}
