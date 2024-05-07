using FluentValidation;

namespace Test.Application.Categories.Commands.Create
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage("The Name is required.");
        }
    }
}
