using AutoMapper;
using Test.Domain.Entities;

namespace Test.Application.Products.Commands.Create
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Product, ProductDTO>();
            }
        }
    }
}
