using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Infrastructure.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Name).IsRequired().HasMaxLength(320);

            builder.ToTable("permission");
        }
    }
}
