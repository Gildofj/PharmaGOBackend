using FluentValidation;

namespace PharmaGO.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
  public CreateProductCommandValidator()
  {
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.Amount).NotEmpty();
    RuleFor(x => x.Description).Length(0, 300);
  }
}