using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.Application.Common.Models;
using Test.Application.Interfaces;
using Test.Domain.Extensions;

namespace Test.Application.Users.Queries.Login
{
    public class LoginQuery : IRequest<LoginDTO>
    {
        public LoginQuery(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; init; }
        public string Password { get; init; }
    }

    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public LoginQueryHandler(IApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<LoginDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(c => c.UserRoles).ThenInclude(c => c.Role).ThenInclude(c => c.RolePermissions).ThenInclude(c => c.Permission).FirstOrDefaultAsync(p => p.Email == request.Username && p.Password == request.Password.ComputeSha256Hash());
            if (user == null)
            {
                throw new ArgumentException("The email or password is invalid.");
            }
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.Username ?? Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }

            var permissions = user.UserRoles.Select(c => c.Role.RolePermissions.Select(c => c.Permission.Name)).SelectMany(c => c);
            foreach (var permission in permissions.Distinct())
            {
                claims.Add(new Claim(ClaimTypes.AuthenticationMethod, permission));
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtConfig.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(8);
            var token = new JwtSecurityToken(
               issuer: _appSettings.JwtConfig.Issuer,
               audience: _appSettings.JwtConfig.Audience,
               claims: claims,
               expires: expires, // Token expiration time
               signingCredentials: creds
           );
            return new LoginDTO(new JwtSecurityTokenHandler().WriteToken(token), expires);
        }
    }
}
