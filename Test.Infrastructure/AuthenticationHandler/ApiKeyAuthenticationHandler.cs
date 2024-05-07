using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Test.Application.Users.Queries.ValidateApiAccess;

namespace Test.Infrastructure.AuthenticationHandler
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string ApiKeyHeaderName = "Authorization";
        private readonly ISender _sender;
        public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISender sender) : base(options, logger, encoder)
        {
            _sender = sender;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
                {
                    return AuthenticateResult.Fail("API Key was not provided.");
                }

                var providedApiKey = apiKeyHeaderValues.FirstOrDefault()?.Replace("basic ", "", StringComparison.InvariantCultureIgnoreCase);
                if (string.IsNullOrEmpty(providedApiKey))
                {
                    return AuthenticateResult.Fail("Invalid API Key.");
                }

                var isLogined = await _sender.Send(new ValidateApiAccessQuery(providedApiKey));
                if (isLogined)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, "API User") };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
                return AuthenticateResult.Fail("Invalid API Key provided.");
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid API Key provided.");
            }
        }
    }
}
