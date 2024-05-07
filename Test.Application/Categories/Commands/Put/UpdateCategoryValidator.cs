using FluentValidation;

namespace Test.Application.Categories.Commands.Put
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage("The Name is required.");
        }
    }
}
