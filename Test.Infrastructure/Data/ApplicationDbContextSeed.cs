using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Test.Domain.Constants;
using Test.Domain.Entities;
using Test.Domain.Extensions;

namespace Test.Infrastructure.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedData(this ApplicationDbContext context, IConfiguration configuration)
        {
            var permission = new List<Permission>()
            {
                new Permission("Create"),
                new Permission("Delete"),
                new Permission("Get"),
                new Permission("Edit")
            };

            foreach (var item in permission)
            {
                var source = await context.Permissions.FirstOrDefaultAsync(p => p.Name == item.Name);
                if (source == null)
                {
                    await context.Permissions.AddAsync(item);
                }
            }


            var roles = new List<Role>()
            {
                new Role("Reader"),
                new Role("Admin")
            };

            foreach (var role in roles)
            {
                var source = await context.Roles.FirstOrDefaultAsync(p => p.Name == role.Name);
                if (source == null)
                {
                    await context.Roles.AddAsync(role);
                }
            }

            var users = new List<User>()
            {
                new User("haquanghuy1205@gmail.com", "haquanghuy1205"),
            };
            foreach (var user in users)
            {
                var source = await context.Users.FirstOrDefaultAsync(p => p.Email == user.Email);
                if (source == null)
                {
                    await context.Users.AddAsync(user);
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedApiKeyAsync(this ApplicationDbContext context, IConfiguration configuration)
        {
            var apiToken = await context.ApiTokens.FirstOrDefaultAsync();
            if (apiToken == null)
            {
                var token = new ApiToken(Guid.NewGuid().ToString().ComputeSha256Hash(), null, SystemSettingName.ApiKey, false);
                await context.ApiTokens.AddAsync(token);
                await context.SaveChangesAsync();
            }
        }
    }
}
