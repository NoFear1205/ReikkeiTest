using Test.Domain.Common;

namespace Test.Domain.Entities
{
    public class Product : BaseEntity
    {
        private Product() { }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public ICollection<ProductCategory> ProductCategories { get; private set; } = new List<ProductCategory>();

        public void Modify(string name, decimal price, List<int> categoryIds)
        {
            Name = name;
            Price = price;
            ProductCategories = categoryIds.Select(c => new ProductCategory(c)).ToList();
        }
    }
}
