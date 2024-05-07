using FluentValidation;

namespace Test.Application.Users.Queries.Login
{
    public class LoginValidator : AbstractValidator<LoginQuery>
    {
        public LoginValidator()
        {
            RuleFor(v => v.Password)
                .NotEmpty()
                .WithMessage("The password is required.");
        }
    }
}
