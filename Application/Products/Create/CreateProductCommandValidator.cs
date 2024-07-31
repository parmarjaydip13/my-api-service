using FluentValidation;

namespace Application.Products.Create;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Product name is required");

        RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
    }
}
