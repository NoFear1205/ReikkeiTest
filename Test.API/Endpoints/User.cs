using FluentValidation;
using Test.Application.Users.Queries.Information;
using Test.Application.Users.Queries.Login;

namespace Test.API.Endpoints
{
    public class User : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
               .RequireAuthorization()
               .MapGet(GetMe);
        }

        public async Task<IResult> GetMe(ISender sender, [AsParameters] GetUserInformationQuery query, CancellationToken cancellation)
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
