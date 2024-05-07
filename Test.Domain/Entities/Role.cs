using Test.Domain.Common;

namespace Test.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Role() { }
        public Role(string name) 
        {
            Name = name;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
        public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
    }
}
