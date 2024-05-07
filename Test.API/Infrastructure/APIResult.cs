using Test.Application.Common.Models;

namespace Test.API.Infrastructure
{
    public static class APIResult
    {
        public static IResult Ok<T>(T data, string message = "")
        {
            return Results.Ok(new Result<T>(data, message));
        }
        public static IResult BadRequest()
        {
            return Results.BadRequest("The system process failure, Please try later!");
        }
    }
}
