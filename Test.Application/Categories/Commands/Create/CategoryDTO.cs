using AutoMapper;
using Test.Domain.Entities;

namespace Test.Application.Categories.Commands.Create
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Category, CategoryDTO>();
            }
        }
    }
}
