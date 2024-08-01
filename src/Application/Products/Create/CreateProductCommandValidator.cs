using Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;

namespace Application.Products.Create;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty().NotNull().WithMessage(x => "{PropertyName} is required");

        RuleFor(x => x.Price)
            .NotEmpty().NotNull().GreaterThan(0).WithMessage(x => "{PropertyName} is not valid");

        RuleFor(x => x.Category)
            .NotNull().WithMessage(x => "{PropertyName} is required.")
            .IsInEnum().WithMessage(x => " {PropertyName} is not valid.");
    }
}
