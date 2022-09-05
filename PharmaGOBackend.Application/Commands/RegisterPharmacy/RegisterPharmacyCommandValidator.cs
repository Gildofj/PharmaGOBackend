using FluentValidation;

namespace PharmaGOBackend.Application.Commands.RegisterPharmacy;

public class RegisterPharmacyCommandValidator : AbstractValidator<RegisterPharmacyCommand>
{
  public RegisterPharmacyCommandValidator()
  {
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.Cnpj).NotEmpty();
  }
}