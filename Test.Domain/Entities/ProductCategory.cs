using Test.Domain.Common;

namespace Test.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        public ProductCategory() { }
        public ProductCategory(int categoryId) 
        {
            CategoryId = categoryId;
        }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
    }
}
