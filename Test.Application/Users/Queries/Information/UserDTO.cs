using AutoMapper;
using Test.Domain.Entities;

namespace Test.Application.Users.Queries.Information
{
    public class UserDTO
    {
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<User, UserDTO>()
                    .ForMember(m => m.Email, m => m.MapFrom(c => c.Email))
                    .ForMember(m => m.Roles, m => m.MapFrom(c => c.UserRoles.Select(c => c.Role.Name)));
            }
        }
    }
}
