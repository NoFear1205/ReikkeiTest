using Test.Domain.Common;
using Test.Domain.Extensions;

namespace Test.Domain.Entities
{
    public class User : BaseEntity
    {
        private User() { }
        public User(string email, string password)
        {
            Email = email;
            Password = password.ComputeSha256Hash();
        }
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
    }
}
