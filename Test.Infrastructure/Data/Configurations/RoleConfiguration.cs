using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Name).IsRequired().HasMaxLength(320);

            builder.ToTable("role");
        }
    }
}
