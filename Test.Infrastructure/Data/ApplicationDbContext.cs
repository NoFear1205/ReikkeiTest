using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using Test.Application.Interfaces;
using Test.Domain.Entities;

namespace Test.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<ApiToken> ApiTokens => Set<ApiToken>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();

        public IDbContextTransaction BeginTransaction() => this.Database.BeginTransaction();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
