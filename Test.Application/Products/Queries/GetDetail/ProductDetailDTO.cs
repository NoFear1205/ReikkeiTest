using AutoMapper;
using Test.Domain.Entities;

namespace Test.Application.Products.Queries.Get
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<KeyValuePair<int, string>> Categories { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Product, ProductDetailDTO>()
                    .ForMember(m => m.Categories, m => m.MapFrom(c => c.ProductCategories.Select(c => new KeyValuePair<int, string>(c.CategoryId, c.Category.Name))));
            }
        }
    }
}
