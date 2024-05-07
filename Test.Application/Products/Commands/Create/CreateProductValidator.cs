using FluentValidation;

namespace Test.Application.Products.Commands.Create
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage("The Name is required.");
            RuleFor(v => v.Price)
                .NotEmpty()
                .WithMessage("The Price is required.");
        }
    }
}
