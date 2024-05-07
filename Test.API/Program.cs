using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using Test.API;
using Test.Application;
using Test.Infrastructure;
using Test.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins")?.Get<string[]>();
builder.Services.AddCors(opt => opt.AddPolicy("webApi", c =>
{
    c.SetIsOriginAllowed((origin) =>
    {
        // Parse the origin to get the host
        var uri = new Uri(origin);
        // Allow if the origin is explicitly listed or is any localhost port
        return allowedOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase)
               || (allowedOrigins.Contains("localhost", StringComparer.OrdinalIgnoreCase) && uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase));
    });
    c.AllowAnyMethod();
    c.AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHealthChecks("/health");
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseOpenApi();
app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.UseCors("webApi");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.UseAuthorization();

app.MapEndpoints();
app.InitialiseDatabaseAsync().Wait();
app.Run();