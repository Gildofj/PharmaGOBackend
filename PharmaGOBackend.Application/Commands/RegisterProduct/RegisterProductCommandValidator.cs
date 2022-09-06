using FluentValidation;

namespace PharmaGOBackend.Application.Commands.RegisterProduct;

public class RegisterProductCommandValidator : AbstractValidator<RegisterProductCommand>
{
    public RegisterProductCommandValidator()
  {
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.Amount).NotEmpty();
    RuleFor(x => x.Description).Length(0, 300);
  }
}