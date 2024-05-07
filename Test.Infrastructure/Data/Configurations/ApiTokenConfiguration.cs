using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Infrastructure.Data.Configurations
{
    public class ApiTokenConfiguration : IEntityTypeConfiguration<ApiToken>
    {
        public void Configure(EntityTypeBuilder<ApiToken> builder)
        {
            builder.HasKey(e => e.Key);
            builder.Property(e => e.Key)
                    .HasMaxLength(300)
                    .IsRequired();
            builder.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsRequired(false);
            builder.ToTable("api_token");
        }
    }
}
