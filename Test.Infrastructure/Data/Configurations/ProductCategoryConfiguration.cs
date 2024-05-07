using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Infrastructure.Data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(e => new { e.ProductId, e.CategoryId});

            builder.HasOne(t => t.Product)
                .WithMany(t => t.ProductCategories)
                .HasForeignKey(t => t.ProductId);

            builder.HasOne(t => t.Category)
                .WithMany(t => t.ProductCategories)
                .HasForeignKey(t => t.CategoryId);

            builder.ToTable("product_category");
        }
    }
}
