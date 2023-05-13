using FluentValidation;

namespace PharmaGOBackend.Application.Commands.RegisterClient;
public class RegisterCommandValidator : AbstractValidator<RegisterClientCommand>
{
  public RegisterCommandValidator()
  {
    RuleFor(x => x.FirstName).NotEmpty();
    RuleFor(x => x.LastName).NotEmpty();
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.Password).NotEmpty();
    RuleFor(x => x.PharmacyId).NotEmpty();
  }
}