namespace Test.Application.Users.Queries.Login
{
    public class LoginDTO
    {
        public LoginDTO(string token, DateTime expiryDate)
        {
            Token = token;
            ExpiryDate = expiryDate;
        }

        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
