using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Infrastructure.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(t => new { t.UserId, t.RoleId });

            builder.HasOne(t => t.User)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(t => t.UserId);

            builder.HasOne(t => t.Role)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(t => t.RoleId);


            builder.ToTable("user_role");
        }
    }
}
