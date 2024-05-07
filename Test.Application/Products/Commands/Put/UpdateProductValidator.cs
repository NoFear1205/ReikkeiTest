using FluentValidation;

namespace Test.Application.Products.Commands.Put
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
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
