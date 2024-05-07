namespace Test.Domain.Entities
{
    public class ApiToken
    {

        private ApiToken()
        {
        }
        public ApiToken(string key, DateTime? expiryOn, string description, bool isAdmin)
        {
            Key = key;
            ExpiryOn = expiryOn;
            IsAdmin = isAdmin;
            Description = description;
        }

        public string Key { get; private set; }
        public string Description { get; private set; }
        public DateTime? ExpiryOn { get; private set; }
        public bool IsAdmin { get; private set; } = false;
    }
}
