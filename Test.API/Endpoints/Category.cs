using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Test.Application.Categories.Commands.Create;
using Test.Application.Categories.Commands.Delete;
using Test.Application.Categories.Commands.Put;
using Test.Application.Categories.Queries.Get;

namespace Test.API.Endpoints
{
    public class Category : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
               .MapPost(CreateCategory)
               .MapGet(GetCategoy, "{id}")
               .MapDelete(DeleteCategory, "{id}")
               .MapPut(PutCategory, "{id}");

        }

        public async Task<IResult> CreateCategory(ISender sender, CreateCategoryCommand command, CancellationToken cancellation)
        {
            try
            {
                var result = await sender.Send(command, cancellation);
                return APIResult.Ok(result);
            }
            catch (ValidationException ex)
            {
                return APIResult.Ok("", string.Join(", ", ex.Errors.Select(p => p.ErrorMessage)));
            }
            catch (ArgumentException ex)
            {
                return APIResult.Ok("", ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Results.Forbid();
            }
            catch (Exception ex)
            {
                return APIResult.BadRequest();
            }
        }
        public async Task<IResult> GetCategoy(ISender sender, [FromRoute] int id, [AsParameters] GetCategoryDetailQuery query, CancellationToken cancellation)
        {
            try
            {
                var result = await sender.Send(query, cancellation);
                return APIResult.Ok(result);
            }
            catch (ValidationException ex)
            {
                return APIResult.Ok("", string.Join(", ", ex.Errors.Select(p => p.ErrorMessage)));
            }
            catch (ArgumentException ex)
            {
                return APIResult.Ok("", ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Results.Forbid();
            }
            catch (Exception ex)
            {
                return APIResult.BadRequest();
            }
        }
        public async Task<IResult> PutCategory(ISender sender, [FromRoute] int id, UpdateCategoryCommand query, CancellationToken cancellation)
        {
            try
            {
                query.Id = id;
                var result = await sender.Send(query, cancellation);
                return APIResult.Ok(result);
            }
            catch (ValidationException ex)
            {
                return APIResult.Ok<string>("", string.Join(", ", ex.Errors.Select(p => p.ErrorMessage)));
            }
            catch (ArgumentException ex)
            {
                return APIResult.Ok<string>("", ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Results.Forbid();
            }
            catch (Exception ex)
            {
                return APIResult.BadRequest();
            }
        }
        public async Task<IResult> DeleteCategory(ISender sender, [FromRoute] int id, [AsParameters] DeleteCategoryCommand query, CancellationToken cancellation)
        {
            try
            {
                query.Id = id;
                await sender.Send(query, cancellation);
                return APIResult.Ok<string>("", "Delete successfully");
            }
            catch (ValidationException ex)
            {
                return APIResult.Ok<string>("", string.Join(", ", ex.Errors.Select(p => p.ErrorMessage)));
            }
            catch (ArgumentException ex)
            {
                return APIResult.Ok<string>("", ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Results.Forbid();
            }
            catch (Exception ex)
            {
                return APIResult.BadRequest();
            }
        }
    }
}
