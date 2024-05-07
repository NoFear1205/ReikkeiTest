namespace Test.Application.Common.Models
{
    public class AppSettings
    {
        public JwtConfig JwtConfig { get; set; }
    }
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
