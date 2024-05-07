using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Email).IsRequired().HasMaxLength(320);
            builder.Property(t => t.Password).IsRequired().HasMaxLength(1000);

            builder.HasIndex(t => t.Email).IsUnique();

            builder.ToTable("user");
        }
    }
}
