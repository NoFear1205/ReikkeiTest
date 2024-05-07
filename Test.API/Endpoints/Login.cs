using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Test.Application.Users.Queries.Login;

namespace Test.API.Endpoints
{
    public class Login : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
               .MapPost(TokenLogin);

        }

        public async Task<IResult> TokenLogin(ISender sender, LoginQuery query, CancellationToken cancellation)
        {
            try
            {
                var result = await sender.Send(query, cancellation);
                return APIResult.Ok(result);
            }
            catch (ValidationException ex)
            {
                return APIResult.Ok<LoginDTO>(null, string.Join(", ", ex.Errors.Select(p => p.ErrorMessage)));
            }
            catch (ArgumentException ex)
            {
                return APIResult.Ok<LoginDTO>(null, ex.Message);
            }
            catch (Exception ex)
            {
                return APIResult.BadRequest();
            }
        }
    }
}
