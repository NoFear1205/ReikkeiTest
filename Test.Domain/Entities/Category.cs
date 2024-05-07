using Test.Domain.Common;

namespace Test.Domain.Entities
{
    public class Category : BaseEntity
    {
        private Category() { }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<ProductCategory> ProductCategories { get; private set; } = new List<ProductCategory>();
        public void Modify(string name)
        {
            Name = name;
        }
    }
}
