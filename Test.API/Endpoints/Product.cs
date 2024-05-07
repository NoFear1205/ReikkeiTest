using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Test.Application.Products.Commands.Create;
using Test.Application.Products.Commands.Delete;
using Test.Application.Products.Commands.Put;
using Test.Application.Products.Queries.Get;
using Test.Application.Products.Queries.GetList;

namespace Test.API.Endpoints
{
    public class Product : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .RequireAuthorization()
               .MapPost(Create)
               .MapGet(Get, "{id}")
               .MapDelete(Delete, "{id}")
               .MapPut(Put, "{id}")
               .MapGet(GetList);

        }

        public async Task<IResult> Create(ISender sender, CreateProductCommand command, CancellationToken cancellation)
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
        public async Task<IResult> Get(ISender sender, [FromRoute]int id, [AsParameters] GetProductDetailQuery query, CancellationToken cancellation)
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
        public async Task<IResult> Put(ISender sender, [FromRoute] int id, UpdateProductCommand query, CancellationToken cancellation)
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
        public async Task<IResult> Delete(ISender sender, [FromRoute] int id, [AsParameters] DeleteProductCommand query, CancellationToken cancellation)
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
        public async Task<IResult> GetList(ISender sender, [AsParameters] GetListProductQuery query, CancellationToken cancellation)
        {
            try
            {
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
    }
}
