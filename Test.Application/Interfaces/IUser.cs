namespace Test.Application.Interfaces
{
    public interface IUser
    {
        string? Id { get; }
        List<string>? Roles { get; }
        public List<string>? Permissions { get; }
    }
}
