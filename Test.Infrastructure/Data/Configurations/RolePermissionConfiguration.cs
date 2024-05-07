using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Test.Domain.Entities;

namespace Test.Infrastructure.Data.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(t => new { t.PermissionId, t.RoleId });

            builder.HasOne(t => t.Permission)
                .WithMany(t => t.RolePermissions)
                .HasForeignKey(t => t.PermissionId);

            builder.HasOne(t => t.Role)
                .WithMany(t => t.RolePermissions)
                .HasForeignKey(t => t.RoleId);

            builder.ToTable("role_permission");
        }
    }
}
