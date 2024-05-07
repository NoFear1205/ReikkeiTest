using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Name).IsRequired().HasMaxLength(320);
            builder.Property(t => t.Price).IsRequired();

            builder.ToTable("product");
        }
    }
}
