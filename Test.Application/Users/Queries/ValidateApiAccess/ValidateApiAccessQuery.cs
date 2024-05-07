using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Application.Interfaces;
using Test.Domain.Constants;

namespace Test.Application.Users.Queries.ValidateApiAccess
{
    public class ValidateApiAccessQuery : IRequest<bool>
    {
        public ValidateApiAccessQuery(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string ApiKey { get; init; }
    }

    public class ValidateApiAccessQueryHandler : IRequestHandler<ValidateApiAccessQuery, bool>
    {
        private readonly IApplicationDbContext _context;

        public ValidateApiAccessQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ValidateApiAccessQuery request, CancellationToken cancellationToken)
        {
            var tokenApi = await _context.ApiTokens.FirstOrDefaultAsync(p => p.Description == SystemSettingName.ApiKey
                && p.Key == request.ApiKey
                && (p.ExpiryOn == null || p.ExpiryOn >= DateTime.UtcNow));
            if (tokenApi == null)
            {
                return false;
            }
            return true;
        }
    }
}
