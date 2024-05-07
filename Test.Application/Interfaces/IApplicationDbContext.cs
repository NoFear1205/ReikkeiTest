using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Test.Domain.Entities;

namespace Test.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<ApiToken> ApiTokens { get; }
        public DbSet<User> Users { get; }
        public DbSet<Role> Roles { get; }
        public DbSet<Permission> Permissions { get; }
        public DbSet<Product> Products { get; }
        public DbSet<Category> Categories { get; }
        public DbSet<ProductCategory> ProductCategories { get; }
        IDbContextTransaction BeginTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
