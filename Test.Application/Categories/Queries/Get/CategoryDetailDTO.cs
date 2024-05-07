using AutoMapper;
using Test.Domain.Entities;

namespace Test.Application.Categories.Queries.Get
{
    public class CategoryDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Category, CategoryDetailDTO>();
            }
        }
    }
}
