using System.Security.Claims;
using Test.Application.Interfaces;

namespace Test.API.Services
{
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public List<string>? Roles => _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
        public List<string>? Permissions => _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.AuthenticationMethod).Select(c => c.Value).ToList();
    }
}
