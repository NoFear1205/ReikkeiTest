namespace Test.Domain.Entities
{
    public class Permission
    {
        public Permission() { }
        public Permission(string name) 
        {
            Name = name;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
    }
}
